using System.Collections.Generic;
using System.Linq;
using DealFinder.Core.Distance;
using DealFinder.Data.Deals.Repository;
using DealFinder.Data.Users.Service;
using DealFinder.Data.Votes.Service;

namespace DealFinder.Data.Deals.Service
{
    public class DealsMapper
    {
        public static List<DealModel> Map(List<DealRecord> dealRecords, string userIdentifier)
        {
            var response = new List<DealModel>();

            foreach (var dealRecord in dealRecords)
            {
                var positiveVotes = dealRecord.Votes.Where(x => x.Positive).ToList().Count;
                var negativeVotes = dealRecord.Votes.Where(x => !x.Positive).ToList().Count;

                response.Add(new DealModel
                {
                    Id = dealRecord.Identifier,
                    Title = dealRecord.Title,
                    Summary = dealRecord.Summary,
                    DistanceInMeters = dealRecord.DistanceInMeters,
                    Location = new Location
                    {
                        Longitude = dealRecord.Longitude,
                        Latitude = dealRecord.Latitude
                    },
                    User = UserMapper.Map(dealRecord.User),
                    Votes = new VoteDetailsModel
                    {
                        TotalVotes = dealRecord.Votes.Count,
                        PositiveVotes = positiveVotes,
                        NegativeVotes = negativeVotes,
                        FinalScore = positiveVotes - negativeVotes,
                        HasAlreadyVoted = dealRecord.Votes.Any(x => x.User.Identifier.ToString() == userIdentifier)
                    }
                });
            }

            return response;
        }
    }
}