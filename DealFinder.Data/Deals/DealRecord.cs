using System;
using System.ComponentModel.DataAnnotations;

namespace DealFinder.Data.Deals
{
    public class DealRecord
    {
        [Key]
        public Guid Identifier { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public double DistanceInMeters { get; set; }
        public LocationRecord LocationRecord { get; set; }
    }
}