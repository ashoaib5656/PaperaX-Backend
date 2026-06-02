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
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
</head>
<body style=""font-family: 'Inter', 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f4f7f6; margin: 0; padding: 0;"">
    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background-color: #f4f7f6; padding: 40px 20px;"">
        <tr>
            <td align=""center"">
                <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""max-width: 600px; background-color: #ffffff; border-radius: 12px; overflow: hidden; box-shadow: 0 4px 12px rgba(0,0,0,0.05);"">
                    <tr>
                        <td style=""background-color: #4CAF50; padding: 30px 40px; text-align: center;"">
                            <h1 style=""color: #ffffff; margin: 0; font-size: 28px; font-weight: 700; letter-spacing: 1px;"">PaperaX</h1>
                        </td>
                    </tr>
                    <tr>
                        <td style=""padding: 40px; color: #333333; line-height: 1.6;"">
                            <p style=""margin: 0 0 20px 0; font-size: 16px; color: #555555;"">Hello,</p>
                            <p style=""margin: 0 0 20px 0; font-size: 16px; color: #555555;"">You recently requested a One-Time Password (OTP) to securely sign in to your PaperaX account. Please use the verification code below:</p>
                            
                            <div style=""text-align: center; margin: 35px 0;"">
                                <span style=""display: inline-block; font-size: 36px; font-weight: 800; color: #4CAF50; letter-spacing: 8px; padding: 15px 30px; background-color: #e8f5e9; border-radius: 8px; border: 1px solid #c8e6c9;"">{otp}</span>
                            </div>
                            
                            <p style=""margin: 0 0 20px 0; font-size: 16px; color: #555555;"">This code is valid for <strong>5 minutes</strong>. For your security, please do not share this code with anyone.</p>
                            <p style=""margin: 0; font-size: 16px; color: #555555;"">If you did not request this, you can safely ignore this email.</p>
                        </td>
                    </tr>
                    <tr>
                        <td style=""background-color: #f9fbfb; padding: 20px 40px; text-align: center; border-top: 1px solid #eeeeee;"">
                            <p style=""margin: 0; font-size: 12px; color: #999999;"">&copy; {DateTime.UtcNow.Year} PaperaX. All rights reserved.</p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";

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
