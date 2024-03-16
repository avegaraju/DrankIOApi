using Domain.Ports;
using Domain.Users;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Google
{
    public class GoogleApiClient : IGoogleApiClient
    {
        HttpClient _client;
        private string _userProfileUrl = "https://content-people.googleapis.com/v1/people/me?personFields={fields}&sources=READ_SOURCE_TYPE_PROFILE";

        public GoogleApiClient(Func<HttpClient> httpClient)
        {
            
           _client = httpClient();
        }

        async Task<User> IGoogleApiClient.GetUser(string bearerToken)
        {
            var commaSeparatedPersonFields = "names,emailAddress,genders,photos,birthdays";
            _userProfileUrl = _userProfileUrl.Replace("{fields}", commaSeparatedPersonFields);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            var response = await _client.GetAsync(_userProfileUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            UserProfileDto? userProfileDto = JsonSerializer.Deserialize<UserProfileDto>(content);

            if(userProfileDto == null)
            {
                throw new UserInformationNotFoundException($"{nameof(UserProfileDto)} is null.");
            }

            return userProfileDto.ToDomain();
        }
    }
}
