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

@Component({
    selector: 'home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent {
    @Input() dealsModel: DealsModel;
    @ViewChild('map') map: AgmMap;
    currentCoordinates: Location;
    private _dealsService: DealsService;
    private _locationService: LocationService;
    private _dialog: MatDialog;
    private _voteService: VoteService;
    private _userService: UserService;
    private user: User;

    constructor(dealsService: DealsService, locationService: LocationService, dialog: MatDialog, voteService: VoteService, userService: UserService) {
        this._dealsService = dealsService;
        this._locationService = locationService;
        this._dialog = dialog;
        this._voteService = voteService;
        this._userService = userService;
        this.dealsModel = new DealsModel();
        this.currentCoordinates = new Location();

        this.dealsModel.deals = this._dealsService.getLastSavedDeals();

        this._locationService.getCurrentLocation()
        .then((coordinates) => {
            this.findLocation(coordinates);
            this.currentCoordinates = coordinates;
        });

        this.user = this._userService.getPersistedUser();

        this._dealsService.onChange
        .subscribe(() => {
            this.dealsModel.feedback = 'New deals are available! Click here to load them.';
        });
    }

    reloadDeals() {
        console.log('here');
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

            if (this.dealsModel.deals.length === 0)
                this.dealsModel.feedback = 'No deals found!';
        })
        .catch((error) => {
            this.dealsModel.addError(error.message);
        });
    }

    viewDealDetails(deal: Deal) {
        this._dialog.open(DealDetailsModal, {
            width: '80vw',
            data: {deal: deal}
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

        this._voteService.castVote(this.user.identifier, deal.id, true)
        .then((payload) => {
            // TODO: display result on deal
        });
    }

    voteDown(deal) {
        if (!this.user)
            return;

        deal.votes.hasAlreadyVoted = true;
        deal.votes.totalVotes++;
        deal.votes.finalScore--;

        this._voteService.castVote(this.user.identifier, deal.id, false)
        .then((payload) => {
            // TODO: display result on deal
        });
    }
}