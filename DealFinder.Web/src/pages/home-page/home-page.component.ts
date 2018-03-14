import { Component, Input, ViewChild } from '@angular/core';
import { DealsService } from '../../shared/deals/deals.service';
import { Deal } from '../../shared/deals/deal';
import { DealsModel } from './deals.model';
import { LocationService } from '../../shared/location/location.service';
import { Location } from '../../shared/deals/location';
import { MatDialog } from '@angular/material';
import { DealDetailsModal } from './deal-details/deal-details.modal';
import { AgmMap } from '@agm/core';
import { VoteService } from '../../shared/vote/vote.service';
import { UserService } from '../../shared/user/user.service';
import { User } from '../../shared/user/user';
import { DealDetailsService } from "../../shared/deal-details/deal-details.service";

@Component({
    selector: 'home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent {
    @Input() dealsModel: DealsModel;
    @ViewChild('map') map: AgmMap;
    currentCoordinates: Location;
    highestDistance: number;
    originCoordinates: { lat: number; lng: number; };
    destinationCoordinates: { lat: number; lng: number; };
    private user: User;
    private _dealsService: DealsService;
    private _locationService: LocationService;
    private _dialog: MatDialog;
    private _voteService: VoteService;
    private _userService: UserService;
    private _dealDetailsService: DealDetailsService;

    constructor(dealsService: DealsService, locationService: LocationService, dialog: MatDialog, voteService: VoteService, userService: UserService, dealDetailsService: DealDetailsService) {
        this._dealsService = dealsService;
        this._locationService = locationService;
        this._dialog = dialog;
        this._voteService = voteService;
        this._userService = userService;
        this._dealDetailsService = dealDetailsService;
        this.dealsModel = new DealsModel();
        this.currentCoordinates = new Location();
        this.highestDistance = 0;

        this.dealsModel.deals = this._dealsService.getLastSavedDeals();
        this.dealsModel.deals.forEach((deal) => {
            if (deal.distanceInMiles > this.highestDistance)
                this.highestDistance = Math.ceil(deal.distanceInMiles);
        });

        this._locationService.getCurrentLocation()
        .then((coordinates) => {
            this.findLocation(coordinates);
            this.currentCoordinates = coordinates;
            this.originCoordinates = {lat: coordinates.latitude, lng: coordinates.longitude};
        });

        this.user = this._userService.getPersistedUser();

        this._userService.onChange
        .subscribe((user) => {
            this.user = user;
        });

        this._dealsService.onChange
        .subscribe(() => {
            this.dealsModel.feedback = 'New deals are available! Click here to load them.';
        });

        this._dealDetailsService.directionsRequired
        .subscribe((destinationCoordinates: any) => {
            this.destinationCoordinates = destinationCoordinates;
        });
    }

    reloadDeals() {
        this.dealsModel.feedback = "";
        this.dealsModel.deals = this._dealsService.getLastSavedDeals();
    }

    findLocation(coordinates: Location) {
        let userIdentifier = '';

        if (this.user)
            userIdentifier = this.user.identifier;

        this._dealsService.getDealsByLocation(coordinates, userIdentifier)
        .then((payload: Deal[]) => {
            this.dealsModel.deals = payload;

            if (this.dealsModel.deals.length === 0) {
                this.dealsModel.feedback = 'No deals found!';
                return;
            }

            this.highestDistance = 0;
            this.dealsModel.deals.forEach((deal) => {
                if (deal.distanceInMiles > this.highestDistance)
                    this.highestDistance = Math.ceil(deal.distanceInMiles);
            });
        })
        .catch((error) => {
            this.dealsModel.addError(error.message);
        });
    }

    viewDealDetails(deal: Deal, options: { onlyMap: boolean }) {
        this._dialog.open(DealDetailsModal, {
            width: '80vw',
            data: {
                deal: deal,
                currentCoordinates: this.currentCoordinates,
                originCoordinates: this.originCoordinates,
                destinationCoordinates: {lat: deal.location.latitude, lng: deal.location.longitude},
                onlyMap: options ? options.onlyMap : false
            }
        });
    }

    viewYouAreHere() {
        this._dialog.open(DealDetailsModal, {
            width: '80vw',
            data: {message: 'You are here'}
        });
    }

    resizeMap(e) {
        if (e.tab.textLabel === 'Map View')
            this.map.triggerResize();
    }

    voteUp(deal) {
        if (!this.user)
            return;

        deal.votes.hasAlreadyVoted = true;
        deal.votes.totalVotes++;
        deal.votes.finalScore++;
        this._voteService.castVote(this.user.identifier, deal.id, true);
    }

    voteDown(deal) {
        if (!this.user)
            return;

        deal.votes.hasAlreadyVoted = true;
        deal.votes.totalVotes++;
        deal.votes.finalScore--;
        this._voteService.castVote(this.user.identifier, deal.id, false);
    }

    updateResultsRadius(rangeSelected) {
        this.dealsModel.resultsRadius = rangeSelected.value;

        for (let i = this.dealsModel.deals.length - 1; i >= 0; i--) {
            this.dealsModel.deals[i].visible = this.dealsModel.deals[i].distanceInMiles <= this.dealsModel.resultsRadius;
        }
    }

    markAsExpired(deal: Deal) {
        deal.expired = !deal.expired;

        this._dealsService.markAsExpired(deal.id, deal.expired)
        .then(() => {
            this._dealsService.updatePersistedDeal(deal);
        });
    }
}