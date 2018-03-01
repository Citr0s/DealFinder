import { Injectable } from '@angular/core';
import { DealsRepository } from './deals.repository';
import { DealsMapper } from './deals.mapper';
import { GetDealsByLocationResponse } from './getDealsByLocationResponse';
import { Location } from './location';
import { User } from '../user/user';
import { Deal } from './deal';

@Injectable()
export class DealsService {
    private _dealsRepository: DealsRepository;

    constructor(dealsRepository: DealsRepository) {
        this._dealsRepository = dealsRepository;
    }

    getDealsByLocation(location: Location) {
        return new Promise((resolve, reject) => {
            this._dealsRepository.getDealsByLocation(location.latitude, location.longitude)
            .subscribe(
                (payload: GetDealsByLocationResponse) => {
                    let mappedDeals = DealsMapper.map(payload);
                    this.persistDeals(mappedDeals);
                    resolve(mappedDeals);
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

    persistDeals(payload: Deal[]) {
        localStorage.setItem('deals', JSON.stringify(payload));
    }

    getPersistedDeals() {
        return JSON.parse(localStorage.getItem('deals'));
    }

    hasPersistedDeals() {
        return localStorage.getItem('deals') !== null;
    }

    removePersistedDeals() {
        localStorage.removeItem('deals');
    }
}