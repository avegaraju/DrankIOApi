using Domain.Ports;

namespace Google
{
    public class GoogleApiClient : IGoogleApiClient
    {
        HttpClient _client;

        public GoogleApiClient(Func<HttpClient> httpClient)
        {
           _client = httpClient();
        }
        public Task GetUser(string accessCode)
        {
            throw new NotImplementedException();
        }
    }
}
