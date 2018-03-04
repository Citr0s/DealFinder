import { DealRecord } from './deal.record';
import { Error } from '../communication/error';

export class GetDealsByLocationResponse {
    hasError: boolean;
    error: Error;
    deals: DealRecord[];
}