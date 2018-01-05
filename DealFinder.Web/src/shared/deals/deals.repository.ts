import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";

@Injectable()
export class DealsRepository {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;

    }

    getDealsByLocation(latitude: number, longitude: number) {
        return this._httpClient.get(`${environment.backendUrl}deals/${latitude}/${longitude}`);
    }
}