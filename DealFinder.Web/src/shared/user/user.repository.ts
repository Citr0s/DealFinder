import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { RegisterRequest } from "./register-request";

@Injectable()
export class UserRepository {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;

    }

    registerUser(request: RegisterRequest) {
        return this._httpClient.post(`${environment.backendUrl}user/register`, request);
    }
}