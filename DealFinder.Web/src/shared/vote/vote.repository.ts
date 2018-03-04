import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { CastVoteRequest } from "./cast-vote-request";

@Injectable()
export class VoteRepository {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;

    }

    castVote(request: CastVoteRequest) {
        return this._httpClient.post(`${environment.backendUrl}vote/cast`, request);
    }
}