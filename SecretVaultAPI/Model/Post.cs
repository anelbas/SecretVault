using System;
using System.Collections.Generic;

#nullable disable

namespace SecretVaultAPI.Model
{
    public partial class Post
    {
        public Post()
        {
            PostGroups = new HashSet<PostGroup>();
        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? Timestamp { get; set; }
        public int PrivacyStatusId { get; set; }
        public int UserId { get; set; }

        public virtual PrivacyStatus PrivacyStatus { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PostGroup> PostGroups { get; set; }
    }
}
