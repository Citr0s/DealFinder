import { Injectable } from "@angular/core";
import { Location } from "../deals/location";

@Injectable()
export class LocationService {

    getCurrentLocation(): Promise<Location> {
        return new Promise((resolve, reject) => {
            let response = new Location();

            if (!navigator.geolocation) {
                reject(response);
                return;
            }

            navigator.geolocation.getCurrentPosition((position) => {
                response.latitude = position.coords.latitude;
                response.longitude = position.coords.longitude;
                resolve(response);
            });
        });
    }
}