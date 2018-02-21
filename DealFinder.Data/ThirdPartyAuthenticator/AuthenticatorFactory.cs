using DealFinder.Data.ThirdPartyAuthenticator.Google;
using DealFinder.Data.Users.Service;
using RestSharp;

namespace DealFinder.Data.ThirdPartyAuthenticator
{
    public interface IAuthenticatorFactory
    {
        IAuthenticator For(Authenticator authenticator);
    }

    public class AuthenticatorFactory : IAuthenticatorFactory
    {
        public IAuthenticator For(Authenticator authenticator)
        {
            if(authenticator == Authenticator.Google)
                return new GoogleAuthenticator(new RestClient());

            return new UnknownAuthenticator();
        }
    }
}