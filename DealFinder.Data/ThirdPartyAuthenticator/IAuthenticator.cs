namespace DealFinder.Data.ThirdPartyAuthenticator
{
    public interface IAuthenticator
    {
        BaseAuthenticationResponse Authenticate(IAuthenticationRequest request);
    }
}