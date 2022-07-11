using System;

namespace SecretVaultServerless.DTOs
{
  public class PostDTO
  {
    private string Title;
    private string Content;
    private DateTime? Timestamp;
    private string PrivacyStatus;
    private int UserId;

    public PostDTO(string title, string content, DateTime? timestamp, string privacyStatus, int userId)
    {
        Title = title;
        Content = content;
        Timestamp = timestamp;
        PrivacyStatus = privacyStatus;
        UserId = userId;
    }

    public string title { get => Title; set => Title = value; }
    public string content { get => Content; set => Content = value; }
    public DateTime? timestamp { get => Timestamp; set => Timestamp = value; }
    public string privacyStatus { get => PrivacyStatus; set => PrivacyStatus = value; }
    public int userId { get => UserId; set => UserId = value; }
  }
}
