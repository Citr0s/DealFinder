import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { RegisterRequest } from "./register-request";
import { UpdateUserRequest } from "./update-user-request";

@Injectable()
export class UserRepository {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;

    }

    registerUser(request: RegisterRequest) {
        return this._httpClient.post(`${environment.backendUrl}user/register`, request);
    }

    updateUser(request: UpdateUserRequest) {
        return this._httpClient.put(`${environment.backendUrl}user/update`, request);
    }

    deleteUser(userIdentifier: string) {
        return this._httpClient.delete(`${environment.backendUrl}user/delete/${userIdentifier}`);
    }
}