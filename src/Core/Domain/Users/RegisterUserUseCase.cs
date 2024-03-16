using Domain.Ports;
using Domain.Users;
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
        private readonly IGoogleApiClient _googleApiClient;
        private readonly ICognitoClient _cognitoClient;

        public RegisterUserUseCase(
            IGoogleApiClient googleApiClient,
            ICognitoClient cognitoClient
            )
        {
            _googleApiClient = googleApiClient;
            _cognitoClient = cognitoClient;
        }

        public async Task ExecuteAsync(string bearerToken)
        {
            try
            {
                var user = await _googleApiClient.GetUser(bearerToken);
                if (user == null)
                {
                    throw new UserInformationNotFoundException();
                }


            }
            catch (Exception ex ) {
                throw;
            }
        }
    }
}
