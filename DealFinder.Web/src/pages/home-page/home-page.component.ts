import { Component, Input } from '@angular/core';
import { DealsService } from '../../shared/deals/deals.service';
import { Deal } from '../../shared/deals/deal';
import { DealsModel } from './deals.model';

@Component({
    selector: 'home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent {
    @Input() dealsModel: DealsModel;
    private _dealsService: DealsService;

    constructor(dealsService: DealsService) {
        this._dealsService = dealsService;
        this.dealsModel = new DealsModel();

        this.findLocation();
    }

    findLocation() {
        if (!navigator.geolocation)
            return;

        navigator.geolocation.getCurrentPosition((position) => {
            this._dealsService.getDealsByLocation(position.coords.latitude, position.coords.longitude)
            .then((payload: Deal[]) => {
                this.dealsModel.deals = payload;

                if (this.dealsModel.deals.length === 0)
                    this.dealsModel.feedback = 'No deals found!';
            })
            .catch((error) => {
                this.dealsModel.addError(error.message);
            });
        });
    }
}