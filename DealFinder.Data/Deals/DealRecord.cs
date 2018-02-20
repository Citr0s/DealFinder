using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DealFinder.Data.Deals
{
    public class DealRecord
    {
        [Key]
        public Guid Identifier { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        [NotMapped]
        public double DistanceInMeters { get; set; }
        public LocationRecord Location { get; set; }
    }
}