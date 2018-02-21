using DealFinder.Core.Communication;

namespace DealFinder.Data.Users.Repository
{
    public class GetUserResponse : CommunicationResponse
    {
        public UserRecord User { get; set; }
    }
}