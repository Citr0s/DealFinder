import {GetDealsByLocationResponse} from './getDealsByLocationResponse';
import {Deal} from './deal';

export class DealsMapper {
    private static ONE_MILE_IN_METERS: number = 1609;

    static map(payload: GetDealsByLocationResponse): Deal[] {
        let response = [];

        if (payload.hasError)
            return response;

        payload.deals.forEach((item) => {
            response.push({
                title: item.title,
                summary: item.summary,
                distanceDescription: this.generateAppropriateDistanceUnit(item.distanceInMeters),
                location: {
                    latitude: item.location.latitude,
                    longitude: item.location.longitude
                }
            });
        });

        return response;
    }

    private static generateAppropriateDistanceUnit(distanceInMeters: number): string {
        let distanceInMiles = Math.floor(distanceInMeters / this.ONE_MILE_IN_METERS);

        if (distanceInMiles > 0)
            return `${distanceInMiles} mile${distanceInMiles === 1 ? '' : 's'} away`;

        return `${Math.floor(distanceInMeters)} meter${distanceInMeters === 1 ? '' : 's'} away`;
    }
}