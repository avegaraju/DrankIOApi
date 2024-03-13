namespace DrankIO.Domain.Users
{
    public interface IRegisterUserUseCase
    {
        void ExecuteAsync(string email, string accessCode);
    }

    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        public void ExecuteAsync(string email, string accessCode)
        {
            throw new NotImplementedException();
        }
    }
}
