using AuthServer.Models.EmailSender;
using System.Threading.Tasks;

namespace AuthServer.Services.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailSenderRequest mailRequest);
    }
}
