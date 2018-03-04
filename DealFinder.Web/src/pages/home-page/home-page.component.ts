import { Component, Input, ViewChild } from '@angular/core';
import { DealsService } from '../../shared/deals/deals.service';
import { Deal } from '../../shared/deals/deal';
import { DealsModel } from './deals.model';
import { LocationService } from "../../shared/location/location.service";
import { Location } from "../../shared/deals/location";
import { MatDialog } from "@angular/material";
import { DealDetailsModal } from "./deal-details/deal-details.modal";
import { AgmMap } from "@agm/core";
import { VoteService } from "../../shared/vote/vote.service";
import { UserService } from "../../shared/user/user.service";
import { User } from "../../shared/user/user";

@Component({
    selector: 'home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent {
    @Input() dealsModel: DealsModel;
    @ViewChild('map') map: AgmMap;
    private _dealsService: DealsService;
    private _locationService: LocationService;
    private currentCoordinates: Location;
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

        this._locationService.getCurrentLocation()
        .then((coordinates) => {
            this.findLocation(coordinates);
            this.currentCoordinates = coordinates;
        });

        this.user = this._userService.getPersistedUser();
    }

    findLocation(coordinates: Location) {
        this._dealsService.getDealsByLocation(coordinates)
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
        if (e.tab.textLabel === "Map View")
            this.map.triggerResize();
    }

    voteUp(dealId) {
        if (!this.user)
            return;

        this._voteService.castVote(this.user.identifier, dealId, true)
        .then((payload) => {
            // TODO: display result on deal
        });
    }

    voteDown(dealId) {
        if (!this.user)
            return;

        this._voteService.castVote(this.user.identifier, dealId, false)
        .then((payload) => {
            // TODO: display result on deal
        });
    }
}