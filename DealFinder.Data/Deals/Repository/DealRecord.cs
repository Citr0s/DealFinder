using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DealFinder.Data.Tags.Repository;
using DealFinder.Data.Users.Repository;
using DealFinder.Data.Votes.Repository;

namespace DealFinder.Data.Deals.Repository
{
    public class DealRecord
    {
        public DealRecord()
        {
            Votes = new List<VoteRecord>();
            DealTags = new List<DealTagRecord>();
        }

        [Key]
        public Guid Identifier { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public virtual UserRecord User { get; set; }
        public virtual List<VoteRecord> Votes { get; set; }
        public virtual List<DealTagRecord> DealTags { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Expired { get; set; }

        [NotMapped]
        public double DistanceInMeters { get; set; }
    }
}