import { Location } from './location';
import { VoteDetails } from "../vote/vote-details";

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
}