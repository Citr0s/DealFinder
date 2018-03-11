using System;
using DealFinder.Data.Deals.Repository;

namespace DealFinder.Data.Tags.Repository
{
    public class DealTagRecord
    {
        public Guid DealIdentifier { get; set; }
        public DealRecord Deal { get; set; }
        public Guid TagIdentifier { get; set; }
        public TagRecord Tag { get; set; }
    }
}