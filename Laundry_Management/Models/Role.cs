using System;
using System.Collections.Generic;

namespace Laundry_Management.Models
{
    public partial class Role
    {
        public Role()
        {
            RoleUsers = new HashSet<RoleUser>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public ulong? IsActive { get; set; }

        public virtual ICollection<RoleUser> RoleUsers { get; set; }
    }
}
