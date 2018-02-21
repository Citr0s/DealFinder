import {Location} from './location';
import {User} from '../user/user';

export class DealRecord {
    title: string;
    summary: any;
    distanceInMeters: number;
    location: Location;
    user: User;
}