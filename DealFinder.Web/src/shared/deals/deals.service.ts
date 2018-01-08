import {Injectable} from '@angular/core';
import {DealsRepository} from './deals.repository';
import {DealsMapper} from './deals.mapper';
import {GetDealsByLocationResponse} from './getDealsByLocationResponse';

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
                    (payload: GetDealsByLocationResponse[]) => {
                        resolve(DealsMapper.map(payload));
                    },
                    (error) => {
                        reject(error);
                    }
                );
        });
    }
}