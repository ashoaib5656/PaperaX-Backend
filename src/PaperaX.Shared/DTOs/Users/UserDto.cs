namespace PaperaX.Shared.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Gstin { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool HasLocalPassword { get; set; }
        public bool IsDeleted { get; set; }
    }
}
