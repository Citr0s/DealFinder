import { Location } from './location';
import { User } from '../user/user';
import { VoteDetails } from "../vote/vote-details";

export class DealRecord {
    id: string;
    title: string;
    summary: any;
    distanceInMeters: number;
    location: Location;
    user: User;
    votes: VoteDetails;
}