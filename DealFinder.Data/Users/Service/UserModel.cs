using System;

namespace DealFinder.Data.Users.Service
{
    public class UserModel
    {
        public Guid Identifier { get; set; }
        public string UserToken { get; set; }
        public string Username { get; set; }
        public string Picture { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}