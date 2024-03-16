using Domain.Users;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IGoogleApiClient
    {
        Task<User> GetUser(string email);
    }
}
