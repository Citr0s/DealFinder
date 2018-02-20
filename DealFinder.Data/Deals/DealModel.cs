using DealFinder.Core.Distance;

namespace DealFinder.Data.Deals
{
    public class DealModel
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public Location Location { get; set; }
        public double DistanceInMeters { get; set; }
    }
}