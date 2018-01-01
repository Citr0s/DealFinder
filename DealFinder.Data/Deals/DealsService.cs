using System.Collections.Generic;

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
                        Latitude = 52,
                        Longitude = -1.5
                    },
                    Title = "Socks 50% off!"
                },
                new DealModel
                {
                    Location = new Location
                    {
                        Latitude = 50,
                        Longitude = -2
                    },
                    Title = "Books £5 each!"
                }
            };
            
            return deals;
        }
    }
}
