using System;
using System.Collections.Generic;

namespace Laundry_Management.Models
{
    public partial class User
    {
        public User()
        {
            RoleUsers = new HashSet<RoleUser>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? PassHash { get; set; }
        public string? Salt { get; set; }
        public string? PhoneNumber { get; set; }
        public sbyte? AccountType { get; set; }
        public ulong? IsActive { get; set; }
        public ulong? IsLock { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? Money { get; set; }

        public virtual ICollection<RoleUser> RoleUsers { get; set; }
    }
}
