using System;
using System.Collections.Generic;

#nullable disable

namespace SecretVaultAPI.Model
{
    public partial class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
