using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace PaperaX.Infrastructure.Email
{
    public class EmailSettings
    {
        [Required(ErrorMessage = "SmtpServer is required")]
        public string SmtpServer { get; set; } = string.Empty;

        [Range(1, 65535)]
        public int SmtpPort { get; set; }

        [Required(ErrorMessage = "SmtpUsername is required")]
        public string SmtpUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = "SmtpPassword is required")]
        public string SmtpPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "FromEmail is required")]
        [EmailAddress]
        public string FromEmail { get; set; } = string.Empty;
    }
}
