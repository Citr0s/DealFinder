import {GetDealsByLocationResponse} from './getDealsByLocationResponse';
import {Deal} from './deal';
import {DealRecord} from './deal.record';

export class DealsMapper {
    private static ONE_MILE_IN_METERS: number = 1609;

    static map(payload: GetDealsByLocationResponse): Deal[] {
        let response = [];

        if (payload.hasError)
            return response;

        payload.deals.forEach((item: DealRecord) => {
            response.push({
                title: item.title,
                summary: item.summary,
                distanceDescription: this.generateAppropriateDistanceUnit(item.distanceInMeters),
                location: {
                    latitude: item.location.latitude,
                    longitude: item.location.longitude
                },
                user: {
                    identifier: item.user.identifier,
                    picture: item.user.picture,
                    username: item.user.username
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