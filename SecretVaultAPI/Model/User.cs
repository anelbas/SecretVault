using System;
using System.Collections.Generic;

#nullable disable

namespace SecretVaultAPI.Model
{
    public partial class User
    {
        public User()
        {
            GroupUsers = new HashSet<GroupUser>();
            Groups = new HashSet<Group>();
            Posts = new HashSet<Post>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public virtual ICollection<GroupUser> GroupUsers { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
