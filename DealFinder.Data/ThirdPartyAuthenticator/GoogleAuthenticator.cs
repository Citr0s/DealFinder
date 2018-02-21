namespace DealFinder.Data.ThirdPartyAuthenticator
{
    public class GoogleAuthenticator : IAuthenticator
    {
        public IAuthenticationResponse Authenticate(IAuthenticationRequest request)
        {
            var response = new BaseAuthenticationResponse();

            return response;
        }

        BaseAuthenticationResponse IAuthenticator.Authenticate(IAuthenticationRequest request)
        {
            // https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={token}

            throw new System.NotImplementedException();
        }
    }
}