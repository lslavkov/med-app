using System.Threading.Tasks;

namespace Med_App_API.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
}