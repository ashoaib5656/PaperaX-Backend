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
        [Required(ErrorMessage = "ResendApiKey is required")]
        public string ResendApiKey { get; set; } = string.Empty;

        [Required(ErrorMessage = "FromEmail is required")]
        [EmailAddress]
        public string FromEmail { get; set; } = string.Empty;
    }
}
