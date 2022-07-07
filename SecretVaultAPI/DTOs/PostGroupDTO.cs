namespace SecretVaultAPI.DTOs
{
    public class PostGroupDTO
    {
        private int? PostId;
        private int? GroupId;

        public int _postId { get => PostId; set => PostId = value; }
        public int? _groupId { get => GroupId; set => GroupId = value; }
    }

}
