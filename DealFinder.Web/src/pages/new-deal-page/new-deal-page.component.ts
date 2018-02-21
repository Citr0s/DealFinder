import { Component, Input } from '@angular/core';
import { DealsService } from "../../shared/deals/deals.service";
import { Location } from "../../shared/deals/location";

@Component({
    selector: 'new-deal-page',
    templateUrl: './new-deal-page.component.html',
    styleUrls: ['./new-deal-page.component.scss']
})
export class NewDealPageComponent {
    @Input() title: string;
    @Input() summary: string;
    coordinates: Location;
    private _dealsService: DealsService;

    constructor(dealsService: DealsService) {
        this._dealsService = dealsService;
    }

    getCurrentLocation() {
        if (!navigator.geolocation)
            return;

        navigator.geolocation.getCurrentPosition((position) => {
            this.coordinates = {
                latitude: position.coords.latitude,
                longitude: position.coords.longitude
            };
        });
    }

    submit() {
        this._dealsService.saveDeal(this.title, this.summary, this.coordinates)
        .then(() => {
            // TODO: finalise form submission
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
}