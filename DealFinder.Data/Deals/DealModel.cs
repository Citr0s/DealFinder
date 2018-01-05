namespace DealFinder.Data.Deals
{
    public class DealModel
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public Location Location { get; set; }
        public double DistanceInMiles { get; set; }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}