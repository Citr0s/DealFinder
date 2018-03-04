import { Vote } from "./vote";

export class CastVoteRequest {
    vote: Vote;
    positive: boolean;
}