import {Component, Input} from '@angular/core';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    @Input() feedback;

    findLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition((position) => {
                console.log(position);
                this.feedback = `lat: ${position.coords.latitude} lng: ${position.coords.longitude}`;
            });
            return;
        }

        this.feedback = 'Geolocation is not supported by this browser.';
    }
}
