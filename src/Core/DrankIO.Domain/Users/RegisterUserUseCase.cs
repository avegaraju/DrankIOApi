namespace DrankIO.Domain.Users
{
    public interface IRegisterUserUseCase
    {
        void ExecuteAsync(string email, string accessCode);
    }

    internal class RegisterUserUseCase : IRegisterUserUseCase
    {
        public void ExecuteAsync(string email, string accessCode)
        {
            throw new NotImplementedException();
        }
    }
}
