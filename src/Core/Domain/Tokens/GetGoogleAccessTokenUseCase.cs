using Domain.Ports;
using System;
using System.Threading.Tasks;

namespace Domain.Tokens
{
    public interface IGetGoogleAccessTokenUseCase
    {
        Task<string> ExecuteAsync(string credentials);
    }

    public class GetGoogleAccessTokenUseCase : IGetGoogleAccessTokenUseCase
    {
        private readonly ICognitoClient _cognitoClient;

        public GetGoogleAccessTokenUseCase(ICognitoClient cognitoClient)
        {
            _cognitoClient = cognitoClient;
        }

        public async Task<string> ExecuteAsync(string credentials)
        {
            try
            {
                return await _cognitoClient.GetToken(credentials);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
