using System;
using System.ComponentModel.DataAnnotations;

namespace DealFinder.Data.Deals
{
    public class LocationRecord
    {
        [Key]
        public Guid Identifier { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}