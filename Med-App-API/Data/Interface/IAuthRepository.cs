using System.Threading.Tasks;
using Med_App_API.Helper;
using Med_App_API.Models;

namespace Med_App_API.Data
{
    public interface IAuthRepository
    {
        Task<User> GetUser(int id);
        string GenerateUserName(string firstName, string lastName);
        Task<string> GenerateJwtToken(User user);
        Task<UserManagerResponse> GeneratePasswordEmail(User user);
        Task<UserManagerResponse> GenerateConfirmEmail(User user);
        Task<UserManagerResponse> ConfirmPasswordAsync(string id, string token);
        Task<UserManagerResponse> ConfirmEmailAsync(string id, string token);
    }
}