import {Injectable, Pipe, PipeTransform} from '@angular/core';
import {Deal} from '../../shared/deals/deal';

@Pipe({
    name: 'onlyVisibleFilter',
    pure: false
})
@Injectable()
export class OnlyVisibleFilter implements PipeTransform {
    transform(deals: Deal[], args: any[]): any {
        return deals.filter(deal => deal.expired === false && deal.visible === true);
    }
}