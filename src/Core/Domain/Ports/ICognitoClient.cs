using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface ICognitoClient
    {
        Task<string> GetToken(string accessCode);
        Task RegisterUser(string emailAddress, string userId);
    }
}
