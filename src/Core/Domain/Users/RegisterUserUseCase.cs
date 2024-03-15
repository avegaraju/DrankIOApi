using Domain.Ports;
using DrankIO.Domain.Ports;

namespace DrankIO.Domain.Users
{
    public interface IRegisterUserUseCase
    {
        Task ExecuteAsync(string email, string accessCode);
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

        public Task ExecuteAsync(string email, string accessCode)
        {
            try
            {
                _googleApiClient.GetUser(email);
            }
            catch ( Exception ex ) {
                throw;
            }

            return Task.CompletedTask;
        }
    }
}
