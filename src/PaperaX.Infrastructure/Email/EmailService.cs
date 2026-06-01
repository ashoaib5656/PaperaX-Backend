using PaperaX.Application.Features.Auth.Interfaces;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace PaperaX.Infrastructure.Email
{
    public class EmailService: IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendOtpAsync(string email, string otp)
        {
            var subject = "PaperaX OTP Verification";

            var body = $@"
                <h1>One-Time Password (OTP) for PaperaX</h1>
                <p>Your OTP is: <strong>{otp}</strong></p>
                <p>This OTP is valid for 5 minutes. Please do not share it with anyone.</p>";

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeKit.MimeMessage();

            email.From.Add(MailboxAddress.Parse(_emailSettings.FromEmail));

            email.To.Add(MailboxAddress.Parse(to));

            email.Subject = subject;

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);

        }
    }
}
