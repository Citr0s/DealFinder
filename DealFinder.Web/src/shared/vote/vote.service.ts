import { Injectable } from "@angular/core";
import { VoteRepository } from "./vote.repository";
import { CastVoteResponse } from "./cast-vote-response";

@Injectable()
export class VoteService {
    private _voteRepository: VoteRepository;

    constructor(voteRepository: VoteRepository) {
        this._voteRepository = voteRepository;
    }

    castVote(userIdentifier: string, dealIdentifier: string): Promise<CastVoteResponse> {
        return new Promise((resolve, reject) => {
            let request = {
                vote: {
                    userId: userIdentifier,
                    dealId: dealIdentifier
                }
            };
            this._voteRepository.castVote(request)
            .subscribe((payload: CastVoteResponse) => {
                resolve(payload);
            }, (error) => {
                reject(error);
            });
        });
    }
}