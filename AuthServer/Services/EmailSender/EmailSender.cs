using AuthServer.Configurations;
using AuthServer.Models.EmailSender;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.IO;
using System.Threading.Tasks;

namespace AuthServer.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderConfig _emailConfig;

        public EmailSender(IOptions<EmailSenderConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task SendEmailAsync(EmailSenderRequest mailRequest)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(_emailConfig.Mail);
            if (!string.IsNullOrEmpty(_emailConfig.DisplayName))
                email.Sender.Name = _emailConfig.DisplayName;

            email.From.Add(email.Sender);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            
            smtp.Connect(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailConfig.Mail, _emailConfig.Password);
           
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
