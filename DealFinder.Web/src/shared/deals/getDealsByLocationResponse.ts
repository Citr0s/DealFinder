export class GetDealsByLocationResponse {
    title: string;
    distanceInMiles: number;
    location: Location;
}

export class Location {
    latitude: number;
    longitude: number;
}