using System.Net;
using System.Net.Mail;
using waytodine_sem9.Services.admin.adminInterfaces;

namespace waytodine_sem9.Services.admin.adminClasses
{
    public class EmailService:IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {

            var smtpServer = _configuration["EmailSettings:SMTPServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SMTPPort"]);
            var smtpUsername = _configuration["EmailSettings:SMTPUsername"];
            var smtpPassword = _configuration["EmailSettings:SMTPPassword"];
            var fromEmail = _configuration["EmailSettings:FromEmail"];



            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;


                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);
                try
                {
                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine($"Email sent to {email} with subject '{subject}'");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                    throw;
                }
            }


                // Implement email sending logic here (e.g., using SMTP, SendGrid, etc.)
                Console.WriteLine($"Sending email to {email} with subject '{subject}' and message '{message}'");
        }
    }
}
