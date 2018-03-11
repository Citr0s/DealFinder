using System.Collections.Generic;
using System.Linq;
using DealFinder.Core.Distance;
using DealFinder.Data.Deals.Repository;
using DealFinder.Data.Users.Service;
using DealFinder.Data.Votes.Service;

namespace DealFinder.Data.Deals.Service
{
    public interface IDealsMapper
    {
        List<DealModel> Map(List<DealRecord> dealRecords, string userIdentifier);
    }

    public class DealsMapper : IDealsMapper
    {
        private readonly IUserMapper _userMapper;

        public DealsMapper(IUserMapper userMapper)
        {
            _userMapper = userMapper;
        }

        public List<DealModel> Map(List<DealRecord> dealRecords, string userIdentifier)
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
                    CreatedAt = dealRecord.CreatedAt,
                    Location = new Location
                    {
                        Longitude = dealRecord.Longitude,
                        Latitude = dealRecord.Latitude
                    },
                    User = _userMapper.Map(dealRecord.User),
                    Votes = new VoteDetailsModel
                    {
                        TotalVotes = dealRecord.Votes.Count,
                        PositiveVotes = positiveVotes,
                        NegativeVotes = negativeVotes,
                        FinalScore = positiveVotes - negativeVotes,
                        HasAlreadyVoted = dealRecord.Votes.Any(x => x.User.Identifier.ToString() == userIdentifier)
                    },
                    Tags = dealRecord.Tags.ConvertAll(x => x.Name)
                });
            }

            return response;
        }
    }
}