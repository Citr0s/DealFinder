using System;
using RestSharp;

namespace DealFinder.Data.ThirdPartyAuthenticator.Google
{
    public class GoogleAuthenticator : IAuthenticator
    {
        private readonly RestClient _httpClient;

        public GoogleAuthenticator()
        {
            _httpClient = new RestClient();
        }

        BaseAuthenticationResponse IAuthenticator.Authenticate(IAuthenticationRequest request)
        {
            var response = new BaseAuthenticationResponse();

            _httpClient.BaseUrl = new Uri("https://www.googleapis.com/");

            var endpointRequest = new RestRequest("oauth2/v3/tokeninfo?id_token={token}", Method.GET);
            endpointRequest.AddUrlSegment("token", request.Token);

            var endpointResponse = _httpClient.Execute<GoogleAuthenticationResponse>(endpointRequest);

            response.UserId = endpointResponse.Data.Sub;
            return response;
        }
    }
}