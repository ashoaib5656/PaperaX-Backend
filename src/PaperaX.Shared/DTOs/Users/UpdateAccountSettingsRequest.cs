using System.ComponentModel.DataAnnotations;

namespace PaperaX.Shared.DTOs.Users
{
    public class UpdateAccountSettingsRequest
    {
        [Required(ErrorMessage = "Full Name is mandatory")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is mandatory")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is mandatory")]
        public string Address { get; set; } = string.Empty;

        // Optional fields
        public string? Company { get; set; }
        public string? Gstin { get; set; }
    }
}
