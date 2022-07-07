namespace SecretVaultAPI.DTOs
{
    public class GroupDTO
    {
        private string? GroupName;
        private int? CreatedBy;

        public string _groupName { get => GroupName; set => GroupName = value; }
        public int? _createdBy { get => CreatedBy; set => CreatedBy = value; }
    }

}
