using Domain.Ports;
using Domain.Users;

namespace Google
{
    public class GoogleApiClient : IGoogleApiClient
    {
        HttpClient _client;

        public GoogleApiClient(Func<HttpClient> httpClient)
        {
           _client = httpClient();
        }

        Task<User> IGoogleApiClient.GetUser(string bearerToken)
        {
            throw new NotImplementedException();
        }
    }
}
