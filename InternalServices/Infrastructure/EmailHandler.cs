using Domain.Models.Email;
using InternalServices.Infrastructure.Abstractions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace InternalServices.Infrastructure
{
    internal class EmailHandler : IEmailHandler
    {
        private readonly EmailConfiguration _configuration;

        public EmailHandler(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }
        public EmailHandler(IOptions<EmailConfiguration> settings)
        {
            _configuration = settings.Value;
        }
        public async Task SendOTPMail(string otp, string email)
        {
            await Task.Run(() =>
            {
                using var client = new SmtpClient();
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Admin",
                _configuration.Sender);
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress("User", email);
                message.To.Add(to);

                message.Subject = "Verification OTP By Spark Identity";
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "Enter the code below to verify your identity\nCode: " + otp;
                message.Body = bodyBuilder.ToMessageBody();
                client.Connect(_configuration.Server, _configuration.Port ?? 0, true);
                client.Authenticate(_configuration.Username, _configuration.Password);
                client.Send(message);
                client.Disconnect(true);
            });
        }
    }
}
