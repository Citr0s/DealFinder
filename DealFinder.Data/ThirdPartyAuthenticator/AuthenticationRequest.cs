namespace DealFinder.Data.ThirdPartyAuthenticator
{
    public interface IAuthenticationRequest
    {
        string Token { get; set; }
    }

    public class AuthenticationRequest : IAuthenticationRequest
    {
        public string Token { get; set; }
    }
}