import { Location } from './location';
import { VoteDetails } from "../vote/vote-details";

export class Deal {
    id: string;
    title: string;
    summary: string;
    distanceDescription: string;
    distanceInMiles: number;
    visible: boolean;
    location: Location;
    votes: VoteDetails;
}