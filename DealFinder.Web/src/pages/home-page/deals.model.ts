import { Deal } from '../../shared/deals/deal';
import { CommonModel } from '../../shared/communication/common.model';

export class DealsModel extends CommonModel {
    feedback: string;
    deals: Deal[];
    resultsRadius: number;

    constructor() {
        super();
        this.deals = [];
        this.feedback = "";
    }
}