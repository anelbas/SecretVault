using System;

namespace SecretVaultAPI.DTOs
{
  public class PostDTO
  {
    private string Title;
    private string Content;
    private string PrivacyStatus;
    private int UserId;

    public PostDTO(string title, string content, string privacyStatus, int userId)
    {
        Title = title;
        Content = content;
        PrivacyStatus = privacyStatus;
        UserId = userId;
    }

    public string title { get => Title; set => Title = value; }
    public string content { get => Content; set => Content = value; }
    public string privacyStatus { get => PrivacyStatus; set => PrivacyStatus = value; }
    public int userId { get => UserId; set => UserId = value; }
  }
}
