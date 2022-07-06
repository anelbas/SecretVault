using System;
using System.Collections.Generic;

#nullable disable

namespace SecretVaultAPI.Model
{
    public partial class GroupUser
    {
        public int GroupUserId { get; set; }
        public int GroupId { get; set; }
        public int UsrId { get; set; }

        public virtual Group Group { get; set; }
        public virtual User Usr { get; set; }
    }
}
