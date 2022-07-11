using System;
using System.Collections.Generic;

#nullable disable

namespace SecretVaultServerless.Model
{
    public partial class PrivacyStatus
    {
        public PrivacyStatus()
        {
            Posts = new HashSet<Post>();
        }

        public int PrivacyStatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
