using System.ComponentModel.DataAnnotations;

namespace PaperaX.Infrastructure.Redis
{
    public class RedisSettings
    {
        [Required(ErrorMessage = "Redis ConnectionString is required")]
        public string ConnectionString { get; set; } = string.Empty;
    }
}
