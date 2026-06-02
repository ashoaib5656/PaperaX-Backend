using System.ComponentModel.DataAnnotations;

namespace PaperaX.Infrastructure.Authentication
{
    public class GoogleAuthSettings
    {
        [Required(ErrorMessage = "Google ClientId is required")]
        public string ClientId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Google ClientSecret is required")]
        public string ClientSecret { get; set; } = string.Empty;
    }
}
