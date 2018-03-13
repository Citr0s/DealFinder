using DealFinder.Core.Communication;

namespace DealFinder.Data.Users.Repository
{
    public class UpdateUserResponse : CommunicationResponse
    {
        public UserRecord User { get; set; }
    }
}