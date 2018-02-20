import { Component, Input } from '@angular/core';
import { DealsService } from "../../shared/deals/deals.service";
import { Location } from "../../shared/deals/location";

@Component({
    selector: 'sign-up-page',
    templateUrl: './sign-up-page.component.html',
    styleUrls: ['./sign-up-page.component.scss']
})
export class SignUpPageComponent {

    constructor() {
    }
}