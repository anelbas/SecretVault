using SecretVaultServerless.Model;
using SecretVaultServerless.DTOs;
using System.Linq;

namespace SecretVaultServerless.Adapter
{
    public class ResponseAdapter
    {
        SecretVaultDBContext _context = new SecretVaultDBContext();
        public PostDTO asDTO(Post postToReturn)
        {
            string privacy = _context.PrivacyStatuses.Where(priv => priv.PrivacyStatusId == postToReturn.PrivacyStatusId).ToList().First().Status;
            PostDTO dtoTOReturn = new PostDTO(postToReturn.Title, postToReturn.Content, postToReturn.Timestamp, privacy, postToReturn.UserId);
            return dtoTOReturn;
        }
    }
}
