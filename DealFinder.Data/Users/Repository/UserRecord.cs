using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DealFinder.Data.Deals.Repository;

namespace DealFinder.Data.Users.Repository
{
    public class UserRecord
    {
        public UserRecord()
        {
            Deals = new List<DealRecord>();
        }

        [Key]
        public Guid Identifier { get; set; }
        public string UserToken { get; set; }
        public string Username { get; set; }
        public string Picture { get; set; }
        public virtual List<DealRecord> Deals { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool Active { get; set; }
    }
}