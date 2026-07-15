using System;

namespace PaperaX.Application.Common.Models
{
    public class ClientErrorLogDto
    {
        public string Message { get; set; } = string.Empty;
        public string? Stack { get; set; }
        public string? ComponentStack { get; set; }
        public string? Route { get; set; }
        public string? UserId { get; set; }
        public string? RoleId { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? BoundaryLevel { get; set; }
        public string? BoundaryName { get; set; }
    }
}
