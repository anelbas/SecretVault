using System;
using System.Collections.Generic;

#nullable disable

namespace SecretVaultAPI.Model
{
    public partial class PostGroup
    {
        public int PostGroupId { get; set; }
        public int PostId { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual Post Post { get; set; }
    }
}
