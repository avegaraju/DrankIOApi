using Amazon.CognitoIdentityProvider;
using DrankIO.Domain.Ports;
using System.Threading.Tasks;

namespace DrankIO.Adapters.Cognito
{
    public class CognitoClient : ICognitoClient
    {
        private readonly IAmazonCognitoIdentityProvider _client;

        public CognitoClient(IAmazonCognitoIdentityProvider client)
        {
            this._client = client;
        }

        Task<string> ICognitoClient.GetToken(string accessCode)
        {
            throw new System.NotImplementedException();
        }
    }
}
