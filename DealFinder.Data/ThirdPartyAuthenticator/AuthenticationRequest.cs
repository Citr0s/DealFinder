namespace DealFinder.Data.ThirdPartyAuthenticator
{
    public interface IAuthenticationRequest
    {
    }

    public class AuthenticationRequest : IAuthenticationRequest
    {
        public string Token { get; set; }
    }
}