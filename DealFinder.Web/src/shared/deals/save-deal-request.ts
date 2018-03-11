export class SaveDealRequest {
    title: string;
    summary: string;
    location: {
        latitude: number;
        longitude: number;
    };
    userIdentifier: string;
    tags: string[];
}