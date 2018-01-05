export class GetDealsByLocationResponse {
    title: string;
    summary: any;
    distanceInMiles: number;
    location: Location;
}

export class Location {
    latitude: number;
    longitude: number;
}