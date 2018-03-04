import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { SaveDealRequest } from "./save-deal-request";

@Injectable()
export class DealsRepository {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;

    }

    getDealsByLocation(latitude: number, longitude: number, userIdentifier: string) {
        return this._httpClient.get(`${environment.backendUrl}deal/${latitude}/${longitude}/${userIdentifier}`);
    }

    saveDeal(request: SaveDealRequest) {
        return this._httpClient.post(`${environment.backendUrl}deal`, request);
    }
}