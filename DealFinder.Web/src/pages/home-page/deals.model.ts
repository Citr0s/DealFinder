import {Deal} from '../../shared/deals/deal';
import {CommonModel} from '../../shared/communication/common.model';

export class DealsModel extends CommonModel {
    constructor() {
        super();
        this.deals = [];
    }

    deals: Deal[];
}