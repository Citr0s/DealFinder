import { Component, Input } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";

@Component({
    selector: 'home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent {
    @Input() feedback;
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;

    }

    findLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition((position) => {
                this._httpClient
                .get(`${environment.backendUrl}deals/${position.coords.latitude}/${position.coords.longitude}`)
                .subscribe((payload) => {
                    this.feedback = JSON.stringify(payload, null, 2);
                });
            });
            return;
        }

        this.feedback = 'Geolocation is not supported by this browser.';
    }
}