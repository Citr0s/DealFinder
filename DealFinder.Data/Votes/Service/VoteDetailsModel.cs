namespace DealFinder.Data.Votes.Service
{
    public class VoteDetailsModel
    {
        public int TotalVotes { get; set; }
        public int PositiveVotes { get; set; }
        public int NegativeVotes { get; set; }
        public int FinalScore { get; set; }
        public bool HasAlreadyVoted { get; set; }
    }
}