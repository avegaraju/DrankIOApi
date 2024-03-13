namespace DrankIO.Domain.Ports
{
    public interface ICognitoClient
    {
        void RegisterUser(string email);
    }
}
