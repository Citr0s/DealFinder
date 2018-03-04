import { Location } from './location';
import { User } from '../user/user';

export class DealRecord {
    id: string;
    title: string;
    summary: any;
    distanceInMeters: number;
    location: Location;
    user: User;
}