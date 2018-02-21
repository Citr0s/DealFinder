using DealFinder.Core.Communication;

namespace DealFinder.Data.Users.Service
{
    public class RegisterResponse : CommunicationResponse
    {
        public UserModel User { get; set; }
    }
}