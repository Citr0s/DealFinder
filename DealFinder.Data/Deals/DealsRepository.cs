using System.Collections.Generic;
using DealFinder.Core.Distance;
using DealFinder.Core.Location;

namespace DealFinder.Data.Deals
{
    public interface IDealsRepository
    {
        GetByLocationResponse GetByLocation(double latitude, double longitude);
    }

    public class DealsRepository : IDealsRepository
    {
        public GetByLocationResponse GetByLocation(double latitude, double longitude)
        {
            var deals =  new List<DealRecord>
            {
                new DealRecord
                {
                    Location = new Location
                    {
                        Latitude = 50,
                        Longitude = -2
                    },
                    DistanceInMeters = Haversine.Calculate(latitude, longitude, 50, -2),
                    Title = "Books £5 each!",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ab corporis deserunt excepturi harum libero nihil porro ratione sapiente sint. Alias amet eligendi enim eveniet illo natus reprehenderit similique soluta sunt."
                },
                new DealRecord
                {
                    Location = new Location
                    {
                        Latitude = 56,
                        Longitude = -2.5
                    },
                    DistanceInMeters = Haversine.Calculate(latitude, longitude, 56, -2.5),
                    Title = "Socks 50% off!",
                    Summary = "Aliquam amet aspernatur at autem consectetur consequuntur delectus doloremque esse excepturi fuga illo laboriosam nostrum obcaecati omnis pariatur porro praesentium quae quasi rem sit soluta sunt, tempore tenetur vitae voluptatum."
                },
                new DealRecord
                {
                    Location = new Location
                    {
                        Latitude = 53.0027,
                        Longitude = -2.1794
                    },
                    DistanceInMeters = Haversine.Calculate(latitude, longitude, 53.0027, -2.1794),
                    Title = "Great deal on electronics",
                    Summary = "Ab at corporis id, laboriosam mollitia numquam odit provident repellat sapiente vel! Architecto numquam similique tempore? Architecto dicta est in iusto natus praesentium quae sunt suscipit ut veniam? Ea, quos."
                },
                new DealRecord
                {
                    Location = new Location
                    {
                        Latitude = 52.8094,
                        Longitude = -1.6428
                    },
                    DistanceInMeters = Haversine.Calculate(latitude, longitude, 52.8094, -1.6428),
                    Title = "Apple Macbook Pro £200 off at Currys",
                    Summary = "Cupiditate ea eos iste praesentium similique? At est facilis hic ipsam nulla optio quia saepe totam! Adipisci assumenda eius exercitationem facere. Accusantium aliquam, exercitationem minima quia similique suscipit tenetur voluptatem!"
                }
            };

            return new GetByLocationResponse
            {
                Deals = deals
            };
        }
    }
}