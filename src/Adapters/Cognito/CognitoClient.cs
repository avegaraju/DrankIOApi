using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Domain.Ports;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Cognito;

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
            var exchangeRequest = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_SRP_AUTH,
                AuthParameters = new Dictionary<string, string>
            {
                {"USERNAME", "google_user"},
                {"PASSWORD", accessCode}
            },
                ClientId = _settings.CognitoAppPoolClientId
            };

            try
            {
                var response = await _client.InitiateAuthAsync(exchangeRequest);
                return response.AuthenticationResult.AccessToken;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
