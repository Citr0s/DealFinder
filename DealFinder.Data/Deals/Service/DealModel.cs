using System;
using System.Collections.Generic;
using DealFinder.Core.Distance;
using DealFinder.Data.Users.Service;
using DealFinder.Data.Votes.Service;

namespace DealFinder.Data.Deals.Service
{
    public class DealModel
    {
        public DealModel()
        {
            Tags = new List<string>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public Location Location { get; set; }
        public double DistanceInMeters { get; set; }
        public string UserIdentifier { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserModel User { get; set; }
        public VoteDetailsModel Votes { get; set; }
        public bool Expired { get; set; }
    }
}