using System;

namespace DealFinder.Data.Users.Repository
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string userHasAlreadyBeenRegistered)
        {
        }
    }
}