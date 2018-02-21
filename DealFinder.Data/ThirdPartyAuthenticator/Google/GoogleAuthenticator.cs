using System;
using DealFinder.Core.Communication;
using RestSharp;

namespace DealFinder.Data.ThirdPartyAuthenticator.Google
{
    public class GoogleAuthenticator : IAuthenticator
    {
        private readonly IRestClient _httpClient;

        public GoogleAuthenticator(IRestClient httpClient)
        {
            _httpClient = httpClient;
        }

        BaseAuthenticationResponse IAuthenticator.Authenticate(IAuthenticationRequest request)
        {
            var response = new BaseAuthenticationResponse();

            _httpClient.BaseUrl = new Uri("https://www.googleapis.com/");

            var endpointRequest = new RestRequest("oauth2/v3/tokeninfo?id_token={token}", Method.GET);
            endpointRequest.AddUrlSegment("token", request.Token);

            var endpointResponse = _httpClient.Execute<GoogleAuthenticationResponse>(endpointRequest);

            if (endpointResponse.Data.ErrorDescription != null)
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.AuthenticatorError,
                    UserMessage = "Something went wrong when validating your credentials. Please try again later.",
                    TechnicalMessage = $"Following error was returned from Google: {endpointResponse.Data.ErrorDescription}"
                });
                return response;
            }

            response.UserId = endpointResponse.Data.Sub;
            response.Username = endpointResponse.Data.Name;
            response.Picture = endpointResponse.Data.Picture;
            return response;
        }
    }
}