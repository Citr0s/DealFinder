using DealFinder.Core.Communication;

namespace DealFinder.Data.ThirdPartyAuthenticator
{
    public class UnknownAuthenticator : IAuthenticator
    {
        public BaseAuthenticationResponse Authenticate(IAuthenticationRequest request)
        {
            var baseAuthenticationResponse = new BaseAuthenticationResponse();

            baseAuthenticationResponse.AddError(new Error
            {
                Code = ErrorCodes.UnknownAuthenticator,
                UserMessage = "Authenticator was not recognised.",
                TechnicalMessage = "Provided authenticator is not currently implemented."
            });

            return baseAuthenticationResponse;
        }
    }
}