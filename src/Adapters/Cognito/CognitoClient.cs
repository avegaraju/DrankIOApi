using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Domain.Ports;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Cognito;
using Amazon.Runtime.Internal.Transform;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using System.Text.Json;

namespace Cognito
{
    public class CognitoClient : ICognitoClient
    {
        private readonly IAmazonCognitoIdentityProvider _client;
        private readonly CognitoSettings _settings;

        public CognitoClient(
            IAmazonCognitoIdentityProvider client,
            CognitoSettings settings
            )
        {
            this._client = client;
            this._settings = settings;
        }

        public Task RegisterUser(string emailAddress, string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetToken(string accessCode)
        {
            var tokenUrl = $"https://<your-cognito-domain>/oauth2/token";

            var content = new StringContent($"grant_type=authorization_code&client_id={_settings.CognitoAppPoolClientId}&client_secret={_settings.CognitoAppPoolClientSecret}&code={accessCode}&redirect_uri={_settings.RedirectUri}", Encoding.UTF8, "application/x-www-form-urlencoded");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(tokenUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(responseContent);
                var root = jsonDocument.RootElement;
                if (root.TryGetProperty("access_token", out var accessToken))
                {
                    return accessToken.GetString();
                }
                else
                {
                    Console.WriteLine("Failed to exchange code for token.");
                    return null;
                }
            }
          
        }
    }
}
