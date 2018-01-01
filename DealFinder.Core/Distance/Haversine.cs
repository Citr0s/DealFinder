using System;

namespace DealFinder.Core.Distance
{
    public static class Haversine
    {
        private const int RadiusOfEarth = 3956;

        public static double Calculate(double firstLatitude, double firstLongitude, double secondLatitude, double secondLongitude)
        {
            var longitudeDistance = ToRadians(secondLongitude - firstLongitude);
            var latitudeDistance = ToRadians(secondLatitude - firstLatitude);
            
            var a = Math.Pow(Math.Sin(latitudeDistance / 2.0), 2) + Math.Cos(ToRadians(firstLatitude)) * Math.Cos(ToRadians(secondLatitude)) * Math.Pow(Math.Sin(longitudeDistance / 2.0), 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return Math.Round(RadiusOfEarth * c, 0);
        }
 
        private static double ToRadians(double angle) {
            return Math.PI * angle / 180.0;
        }
    }
}