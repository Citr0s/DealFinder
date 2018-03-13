import { EventEmitter, Injectable, Output } from '@angular/core';
import { DealsRepository } from './deals.repository';
import { DealsMapper } from './deals.mapper';
import { GetDealsByLocationResponse } from './getDealsByLocationResponse';
import { Location } from './location';
import { Deal } from './deal';
import { DealsComparer } from "./deals.comparer";
import { UserService } from "../user/user.service";
import { LocationService } from "../location/location.service";
import { User } from "../user/user";

@Injectable()
export class DealsService {
    @Output() onChange: EventEmitter<any> = new EventEmitter();
    private _dealsRepository: DealsRepository;
    private _userService: UserService;
    private _locationService: LocationService;

    constructor(dealsRepository: DealsRepository, userService: UserService, locationService: LocationService) {
        this._dealsRepository = dealsRepository;
        this._userService = userService;
        this._locationService = locationService;

        this._userService.onChange
        .subscribe((user: User) => {
            if (user === undefined)
                return;

            this._locationService.getCurrentLocation()
            .then((location: Location) => {
                this.getDealsByLocation(location, user.identifier);
            });
        });
    }

    getDealsByLocation(location: Location, userIdentifier: string) {
        return new Promise((resolve, reject) => {
            if (this.hasPersistedDeals())
                resolve(this.getPersistedDeals());

            this._dealsRepository.getDealsByLocation(location.latitude, location.longitude, userIdentifier)
            .subscribe(
                (payload: GetDealsByLocationResponse) => {
                    let mappedDeals = DealsMapper.map(payload);
                    let shouldEmit = true;

                    if (this.getPersistedDeals() === null)
                        shouldEmit = false;

                    if (!DealsComparer.areEqual(mappedDeals, this.getPersistedDeals()) && shouldEmit)
                        this.onChange.emit();

                    this.persistDeals(mappedDeals);
                    resolve(this.getPersistedDeals());
                },
                (error) => {
                    reject(error);
                }
            );
        });
    }

    saveDeal(title: string, summary: string, coordinates: Location, userIdentifier: string, tags: string[]) {
        return new Promise((resolve, reject) => {
            let request = {
                title: title,
                summary: summary,
                location: {
                    latitude: coordinates.latitude,
                    longitude: coordinates.longitude
                },
                userIdentifier: userIdentifier,
                tags: tags
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

    getLastSavedDeals(): Deal[] {
        if (this.hasPersistedDeals())
            return this.getPersistedDeals();

        return [];
    }

    persistDeals(payload: Deal[]) {
        localStorage.setItem('deals', JSON.stringify(payload));
    }

    getPersistedDeals(): Deal[] {
        return JSON.parse(localStorage.getItem('deals'));
    }

    hasPersistedDeals(): boolean {
        return localStorage.getItem('deals') !== null;
    }

    removePersistedDeals(): void {
        localStorage.removeItem('deals');
    }
}