using System;
using System.Collections.Generic;

#nullable disable

namespace SecretVaultAPI.Model
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? Timestamp { get; set; }
        public int PrivacyStatusId { get; set; }
        public string UserId { get; set; }

        public virtual PrivacyStatus PrivacyStatus { get; set; }
    }
}
