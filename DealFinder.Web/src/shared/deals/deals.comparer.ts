import { Deal } from "./deal";

export class DealsComparer {

    static areEqual(newDeals: Deal[], persistedDeals: Deal[]) {
        if (persistedDeals === null)
            return false;

        return newDeals.length === persistedDeals.length;
    }
}