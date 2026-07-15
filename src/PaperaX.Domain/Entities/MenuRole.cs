using System;

namespace PaperaX.Domain.Entities
{
    public class MenuRole
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; } = null!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public bool CanView { get; set; } = true;
        public bool CanCreate { get; set; } = false;
        public bool CanUpdate { get; set; } = false;
        public bool CanDelete { get; set; } = false;
    }
}
