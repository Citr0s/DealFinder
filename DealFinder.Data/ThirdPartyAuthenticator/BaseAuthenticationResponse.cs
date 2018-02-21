using DealFinder.Core.Communication;

namespace DealFinder.Data.ThirdPartyAuthenticator
{
    public interface IAuthenticationResponse
    {
    }

    public class BaseAuthenticationResponse : CommunicationResponse, IAuthenticationResponse
    {
        public string UserId { get; set; }
    }
}