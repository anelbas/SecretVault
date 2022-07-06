using System;
using System.Collections.Generic;

#nullable disable

namespace SecretVaultAPI.Model
{
    public partial class Group
    {
        public Group()
        {
            GroupUsers = new HashSet<GroupUser>();
            PostGroups = new HashSet<PostGroup>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int CreatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
        public virtual ICollection<PostGroup> PostGroups { get; set; }
    }
}
