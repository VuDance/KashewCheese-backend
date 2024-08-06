using KashewCheese.Application.Common.Interfaces.Email;
using System.Net;
using System.Net.Mail;

namespace KashewCheese.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService()
        {
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("dangdev0402@gmail.com", "erqmuxzzrwrtosxt"),
                EnableSsl = true
            };
        }
        public string GenerateVerificationCode()
        {
            var code = new Random().Next(100000, 999999).ToString();
            return code;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("dangdev0402@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mailMessage.To.Add(to);

            try
            {
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (SmtpException ex)
            {
                // Handle SMTP errors here
                throw new InvalidOperationException("Email sending failed.", ex);
            }
            catch (Exception ex)
            {
                // Handle other errors here
                throw new InvalidOperationException("An error occurred while sending email.", ex);
            }
        }
    }
}
