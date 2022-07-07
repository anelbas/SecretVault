namespace SecretVaultAPI.DTOs
{
    public class GroupUserDTO
    {
        private int? GroupId;
        private int? UsrId;

        public int? _groupId { get => GroupId; set => GroupId = value; }
        public int? _usrId { get => UsrId; set => UsrId = value; }
    }

}
