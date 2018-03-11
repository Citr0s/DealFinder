import { Deal } from "./deal";

export class DealsComparer {

    static areEqual(newDeals: Deal[], persistedDeals: Deal[]) {
        if (persistedDeals === null)
            return false;

        if (newDeals.length !== persistedDeals.length)
            return false;

        for (let i = 0; i < newDeals.length; i++) {
            if (newDeals[i].id !== persistedDeals[i].id)
                return false;

            if (newDeals[i].title !== persistedDeals[i].title)
                return false;

            if (newDeals[i].summary !== persistedDeals[i].summary)
                return false;

            if (newDeals[i].location.latitude !== persistedDeals[i].location.latitude || newDeals[i].location.longitude !== persistedDeals[i].location.longitude)
                return false;

            if (newDeals[i].votes.totalVotes !== persistedDeals[i].votes.totalVotes)
                return false;

            if (newDeals[i].votes.hasAlreadyVoted !== persistedDeals[i].votes.hasAlreadyVoted)
                return false;

            if (newDeals[i].distanceDescription !== persistedDeals[i].distanceDescription)
                return false;
        }

        return true;
    }
}