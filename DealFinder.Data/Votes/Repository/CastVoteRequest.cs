using System;

namespace DealFinder.Data.Votes.Repository
{
    public class CastVoteRequest
    {
        public Guid UserId { get; set; }
        public Guid DealId { get; set; }
    }
}