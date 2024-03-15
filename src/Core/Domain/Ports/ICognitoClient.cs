namespace DrankIO.Domain.Ports
{
    public interface ICognitoClient
    {
        Task RegisterUser(string email);
    }
}
