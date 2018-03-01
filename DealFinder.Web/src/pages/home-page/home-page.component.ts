import { Component, Input, ViewChild } from '@angular/core';
import { DealsService } from '../../shared/deals/deals.service';
import { Deal } from '../../shared/deals/deal';
import { DealsModel } from './deals.model';
import { LocationService } from "../../shared/location/location.service";
import { Location } from "../../shared/deals/location";
import { MatDialog } from "@angular/material";
import { DealDetailsModal } from "./deal-details/deal-details.modal";
import { AgmMap } from "@agm/core";

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

    constructor(dealsService: DealsService, locationService: LocationService, dialog: MatDialog) {
        this._dealsService = dealsService;
        this._locationService = locationService;
        this._dialog = dialog;
        this.dealsModel = new DealsModel();
        this.currentCoordinates = new Location();

        this._locationService.getCurrentLocation()
        .then((coordinates) => {
            this.findLocation(coordinates);
            this.currentCoordinates = coordinates;
        });
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
}