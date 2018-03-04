namespace DealFinder.Data.Votes.Service
{
    public class CastVoteDetailsRequest
    {
        public VoteModel Vote { get; set; }
        public bool Positive { get; set; }
    }
}