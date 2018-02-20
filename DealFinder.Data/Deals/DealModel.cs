namespace DealFinder.Data.Deals
{
    public class DealModel
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public LocationRecord LocationRecord { get; set; }
        public double DistanceInMeters { get; set; }
    }
}