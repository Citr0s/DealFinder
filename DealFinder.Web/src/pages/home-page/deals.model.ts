import { Deal } from '../../shared/deals/deal';
import { CommonModel } from '../../shared/communication/common.model';

export class DealsModel extends CommonModel {
    feedback: string;
    deals: Deal[];

    constructor() {
        super();
        this.deals = [];
        this.feedback = "";
    }
}