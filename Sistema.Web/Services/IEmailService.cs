using System.Threading.Tasks;
namespace Email.Services
{
    public interface IEmailService
    {
        Task SendEmail(string email, string message);
    }
}