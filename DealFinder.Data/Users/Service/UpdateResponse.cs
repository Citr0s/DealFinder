using DealFinder.Core.Communication;

namespace DealFinder.Data.Users.Service
{
    public class UpdateResponse : CommunicationResponse
    {
        public UserModel User { get; set; }
    }
}