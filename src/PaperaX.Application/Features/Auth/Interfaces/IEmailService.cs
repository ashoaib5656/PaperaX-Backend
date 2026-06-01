using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Auth.Interfaces
{
    public interface IEmailService
    {
        Task SendOtpAsync(string email, string otp);
        Task SendEmailAsync(string to, string subject, string body);
    }
}
