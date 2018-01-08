export class GetDealsByLocationResponse {
    hasError: boolean;
    error: Error;
    deals: Deal[];
}

export class Error {
    code: string;
    userMessage: string;
    technicalMessage: string;
}

export class Deal {
    title: string;
    summary: any;
    distanceInMeters: number;
    location: Location;
}

export class Location {
    latitude: number;
    longitude: number;
}