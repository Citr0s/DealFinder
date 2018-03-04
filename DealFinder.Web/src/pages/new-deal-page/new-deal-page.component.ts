import { Component, Input } from '@angular/core';
import { DealsService } from '../../shared/deals/deals.service';
import { Location } from '../../shared/deals/location';
import { UserService } from '../../shared/user/user.service';
import { Router } from '@angular/router';
import { LocationService } from "../../shared/location/location.service";

@Component({
    selector: 'new-deal-page',
    templateUrl: './new-deal-page.component.html',
    styleUrls: ['./new-deal-page.component.scss']
})
export class NewDealPageComponent {
    @Input() title: string;
    @Input() summary: string;
    coordinates: Location;
    initialCoordinates: Location;
    errorMessage: string;
    private _dealsService: DealsService;
    private _userService: UserService;
    private _router: Router;
    private _locationService: LocationService;

    constructor(dealsService: DealsService, userService: UserService, router: Router, locationService: LocationService) {
        this._dealsService = dealsService;
        this._userService = userService;
        this._router = router;
        this._locationService = locationService;
        this.coordinates = new Location();
        this.initialCoordinates = new Location();

        let isUserLoggedIn = this._userService.isLoggedIn();
        if (!isUserLoggedIn)
            this._router.navigate(['']);

        this._locationService.getCurrentLocation()
        .then((coordinates) => {
            this.initialCoordinates = coordinates;
            this.coordinates = coordinates;
        });
    }

    submit() {
        this._dealsService.saveDeal(this.title, this.summary, this.coordinates, this._userService.getPersistedUser().identifier)
        .then((payload: any) => {
            if (payload.hasError) {
                this.errorMessage = payload.error.userMessage;
                return;
            }

            this._router.navigate(['']);
        })
        .catch((error) => {
            // TODO: display error message
        });
    }

    updateTitleValue(event) {
        this.title = event.srcElement.value;
    }

    updateSummaryValue(event) {
        this.summary = event.srcElement.value;
    }

    placeMarker($event: any) {
        this.coordinates = {
            latitude: $event.coords.lat,
            longitude: $event.coords.lng
        };
    }
}