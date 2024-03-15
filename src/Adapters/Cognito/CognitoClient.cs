using Amazon.CognitoIdentityProvider;
using DrankIO.Domain.Ports;
using System.Threading.Tasks;

namespace DrankIO.Adapters.Cognito
{
    public class CognitoClient: ICognitoClient
    {
        private readonly IAmazonCognitoIdentityProvider _client;

        public CognitoClient(IAmazonCognitoIdentityProvider client)
        {
            this._client = client;
        }

        public Task RegisterUser(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task Validate(string accessCode)
        {
            throw new System.NotImplementedException();
        }
    }
}
