namespace DealFinder.Data.Deals
{
    public class DealModel
    {
        public Location Location { get; set; }
        public string Title { get; set; }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}