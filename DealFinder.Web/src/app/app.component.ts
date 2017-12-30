import {Component, Input} from '@angular/core';
import {environment} from '../environments/environment';
import {HttpClient} from '@angular/common/http';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
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
                        console.log(payload);
                        this.feedback = payload;
                    });
            });
            return;
        }

        this.feedback = 'Geolocation is not supported by this browser.';
    }
}
