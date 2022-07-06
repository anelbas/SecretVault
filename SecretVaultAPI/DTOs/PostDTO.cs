using System;

namespace SecretVaultAPI.DTOs
{
  public class PostDTO
  {
    private string Title;
    private string Content;
    private DateTime? Timestamp;
    private int PrivacyStatusId;
    private int UserId;


    public string _title { get => Title; set => Title = value; }
    public string _content { get => Content; set => Content = value; }
    public DateTime? _timestamp { get => Timestamp; set => Timestamp = value; }
    public int _privacyStatusId { get => PrivacyStatusId; set => PrivacyStatusId = value; }
    public int _userId { get => UserId; set => UserId = value; }
  }
}
