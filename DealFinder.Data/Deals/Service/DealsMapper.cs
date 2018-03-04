using System.Collections.Generic;
using DealFinder.Core.Distance;
using DealFinder.Data.Deals.Repository;
using DealFinder.Data.Users.Service;

namespace DealFinder.Data.Deals.Service
{
    public class DealsMapper
    {
        public static List<DealModel> Map(List<DealRecord> dealRecords)
        {
            var response = new List<DealModel>();

            foreach (var dealRecord in dealRecords)
            {
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
                    User = UserMapper.Map(dealRecord.User)
                });   
            }

            return response;
        }
    }
}