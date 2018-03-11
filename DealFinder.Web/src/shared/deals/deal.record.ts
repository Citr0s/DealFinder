import { Location } from './location';
import { User } from '../user/user';
import { VoteDetails } from "../vote/vote-details";

export class DealRecord {
    constructor() {
        this.tags = [];
    }

    id: string;
    title: string;
    summary: any;
    distanceInMeters: number;
    createdAt: Date;
    location: Location;
    user: User;
    votes: VoteDetails;
    tags: string[];
}