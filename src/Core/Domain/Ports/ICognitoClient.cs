using System.Threading.Tasks;

namespace DrankIO.Domain.Ports
{
    public interface ICognitoClient
    {
        Task<string> GetToken(string accessCode);
        Task RegisterUser(string emailAddress, string userId);
    }
}
