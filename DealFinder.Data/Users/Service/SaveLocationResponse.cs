using DealFinder.Core.Communication;

namespace DealFinder.Data.Users.Service
{
    public class SaveLocationResponse : CommunicationResponse
    {
        public UserModel User { get; set; }
    }
}