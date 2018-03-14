import { Location } from './location';
import { VoteDetails } from "../vote/vote-details";
import { User } from "../user/user";

export class Deal {
    constructor() {
        this.tags = [];
    }

    id: string;
    title: string;
    summary: string;
    distanceDescription: string;
    distanceInMiles: number;
    visible: boolean;
    location: Location;
    votes: VoteDetails;
    tags: string[];
    user: User[];
    expired: boolean;
}