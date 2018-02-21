using DealFinder.Core.Distance;
using DealFinder.Data.Users.Service;

namespace DealFinder.Data.Deals.Service
{
    public class DealModel
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public Location Location { get; set; }
        public double DistanceInMeters { get; set; }
        public string UserIdentifier { get; set; }
        public UserModel User { get; set; }
    }
}