using DealFinder.Core.Location;

namespace DealFinder.Data.Deals
{
    public class DealRecord
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public double DistanceInMiles { get; set; }
        public Location Location { get; set; }
    }
}