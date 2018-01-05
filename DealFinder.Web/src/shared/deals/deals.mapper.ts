import { GetDealsByLocationResponse } from "./getDealsByLocationResponse";
import { Deal } from "./deal";

export class DealsMapper {
    static map(payload: GetDealsByLocationResponse[]): Deal[] {
        let response = [];

        payload.forEach((item) => {
            response.push({
                title: item.title,
                distanceInMiles: item.distanceInMiles,
                location: {
                    latitude: item.location.latitude,
                    longitude: item.location.longitude
                }
            });
        });

        return response;
    }
}