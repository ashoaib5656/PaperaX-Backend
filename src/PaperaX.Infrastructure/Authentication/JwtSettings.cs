using System.ComponentModel.DataAnnotations;

namespace PaperaX.Infrastructure.Authentication
{
    public class JwtSettings
    {
        [Required(ErrorMessage = "JWT Secret is required")]
        [MinLength(32, ErrorMessage = "JWT Secret must be at least 32 characters long")]
        public string Secret { get; set; } = string.Empty;

        [Required(ErrorMessage = "JWT Issuer is required")]
        public string Issuer { get; set; } = string.Empty;

        [Required(ErrorMessage = "JWT Audience is required")]
        public string Audience { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int AccessTokenMinutes { get; set; } = 60;

        [Range(1, int.MaxValue)]
        public int RefreshTokenDays { get; set; } = 7;
    }
}
