using Domain.Ports;
using DrankIO.Domain.Ports;

namespace DrankIO.Domain.Users
{
    public interface IRegisterUserUseCase
    {
        void ExecuteAsync(string email, string accessCode);
    }

    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        public RegisterUserUseCase(
            ICognitoClient cognitoClient,
            IGoogleApiClient googleApiClient
            )
        {

        }

        public void ExecuteAsync(string email, string accessCode)
        {
            throw new NotImplementedException();
        }
    }
}
