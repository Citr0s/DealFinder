using System;
using System.ComponentModel.DataAnnotations;

namespace DealFinder.Data.Users.Repository
{
    public class UserRecord
    {
        [Key]
        public Guid Identifier { get; set; }
        public string UserToken { get; set; }
        public string Username { get; set; }
        public string Picture { get; set; }
    }
}