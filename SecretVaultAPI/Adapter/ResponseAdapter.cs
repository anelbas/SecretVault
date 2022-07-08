using SecretVaultAPI.Model;
using SecretVaultAPI.DTOs;
using System.Linq;

namespace SecretVaultAPI.Adapter
{
    public class ResponseAdapter
    {
        SecretVaultDBContext _context = new SecretVaultDBContext();
        public PostDTO asDTO(Post postToReturn)
        {
            string privacy = _context.PrivacyStatuses.Where(priv => priv.PrivacyStatusId == postToReturn.PrivacyStatusId).ToList().First().Status;
            PostDTO dtoTOReturn = new PostDTO(postToReturn.Title, postToReturn.Content, privacy, postToReturn.UserId);
            return dtoTOReturn;
        }
    }
}
