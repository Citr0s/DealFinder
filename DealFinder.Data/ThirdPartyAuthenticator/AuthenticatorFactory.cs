using DealFinder.Data.ThirdPartyAuthenticator.Google;
using DealFinder.Data.Users.Service;

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
                return new GoogleAuthenticator();

            return new UnknownAuthenticator();
        }
    }
}