using Amazon.CognitoIdentityProvider;
using DrankIO.Domain.Ports;

namespace DrankIO.Adapters.Cognito
{
    public class CognitoClient: ICognitoClient
    {
        private readonly IAmazonCognitoIdentityProvider _client;

        public CognitoClient(IAmazonCognitoIdentityProvider client)
        {
            this._client = client;
        }

        public void RegisterUser(string email)
        {

        }
    }
}
