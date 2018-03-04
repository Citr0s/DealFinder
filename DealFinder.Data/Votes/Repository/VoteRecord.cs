using System;
using System.ComponentModel.DataAnnotations;
using DealFinder.Data.Deals.Repository;
using DealFinder.Data.Users.Repository;

namespace DealFinder.Data.Votes.Repository
{
    public class VoteRecord
    {
        [Key]
        public Guid Identifier { get; set; }
        public UserRecord User { get; set; }
        public DealRecord Deal { get; set; }
    }
}