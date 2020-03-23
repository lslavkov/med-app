using System.Threading.Tasks;
using Med_App_API.Models;

namespace Med_App_API.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}