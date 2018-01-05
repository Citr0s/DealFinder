import { Component, Input } from '@angular/core';
import { DealsService } from "../../shared/deals/deals.service";
import { Deal } from "../../shared/deals/deal";

@Component({
    selector: 'home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent {
    @Input() deals: Deal[];
    private _dealsService: DealsService;

    constructor(dealsService: DealsService) {
        this._dealsService = dealsService;
        this.deals = [];
    }

    findLocation() {
        if (!navigator.geolocation)
            return;

        navigator.geolocation.getCurrentPosition((position) => {
            this._dealsService.getDealsByLocation(position.coords.latitude, position.coords.longitude)
            .then((payload: Deal[]) => {
                this.deals = payload;
            })
            .catch((error) => {
                console.error(error);
            });
        });
    }
}