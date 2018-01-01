using System;
using System.Collections.Generic;
using System.Linq;
using DealFinder.Core.Distance;

namespace DealFinder.Data.Deals
{
    public interface IDealsService
    {
        List<DealModel> GetByLocation(double latitude, double longitude);
    }
        

    public class DealsService : IDealsService
    {
        public List<DealModel> GetByLocation(double latitude, double longitude)
        {
            var deals = new List<DealModel>
            {
                new DealModel
                {
                    Location = new Location
                    {
                        Latitude = 50,
                        Longitude = -2
                    },
                    DistanceInMiles = Haversine.Calculate(latitude, longitude, 50, -2),
                    Title = "Books £5 each!"
                },
                new DealModel
                {
                    Location = new Location
                    {
                        Latitude = 56,
                        Longitude = -2.5
                    },
                    DistanceInMiles = Haversine.Calculate(latitude, longitude, 56, -2.5),
                    Title = "Socks 50% off!"
                }
            };

            return deals.OrderBy(x => x.DistanceInMiles).ToList();
        }
    }
}
