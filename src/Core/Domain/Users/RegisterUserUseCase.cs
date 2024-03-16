using Domain.Ports;
using DrankIO.Domain.Ports;
using System;
using System.Threading.Tasks;

namespace DrankIO.Domain.Users
{
    public interface IRegisterUserUseCase
    {
        Task ExecuteAsync(string accessCode);
    }

    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly ICognitoClient _cognitoClient;
        private readonly IGoogleApiClient _googleApiClient;

        public RegisterUserUseCase(
            ICognitoClient cognitoClient,
            IGoogleApiClient googleApiClient
            )
        {
            _cognitoClient = cognitoClient;
            _googleApiClient = googleApiClient;
        }

        public async Task ExecuteAsync(string accessCode)
        {
            try
            {
                var bearerToken = await _cognitoClient.GetToken(accessCode);

                var user = await _googleApiClient.GetUser(bearerToken);
            }
            catch (Exception ex ) {
                throw;
            }
        }
    }
}
