import { Injectable } from '@angular/core';
import { DealsRepository } from './deals.repository';
import { DealsMapper } from './deals.mapper';
import { GetDealsByLocationResponse } from './getDealsByLocationResponse';
import { Location } from "./location";

@Injectable()
export class DealsService {
    private _dealsRepository: DealsRepository;

    constructor(dealsRepository: DealsRepository) {
        this._dealsRepository = dealsRepository;
    }

    getDealsByLocation(latitude: number, longitude: number) {
        return new Promise((resolve, reject) => {
            this._dealsRepository.getDealsByLocation(latitude, longitude)
            .subscribe(
                (payload: GetDealsByLocationResponse) => {
                    resolve(DealsMapper.map(payload));
                },
                (error) => {
                    reject(error);
                }
            );
        });
    }

    saveDeal(title: string, summary: string, coordinates: Location, userIdentifier: string) {
        return new Promise((resolve, reject) => {
            let request = {
                title: title,
                summary: summary,
                location: {
                    latitude: coordinates.latitude,
                    longitude: coordinates.longitude
                },
                userIdentifier: userIdentifier
            };
            this._dealsRepository.saveDeal(request)
            .subscribe(
                (payload) => {
                    resolve(payload);
                },
                (error) => {
                    reject(error);
                }
            );
        });
    }
}