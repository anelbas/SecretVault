using SecretVaultAPI.Model;
using System;
using System.Linq;

namespace SecretVaultAPI.Utils
{
    public class ForeignKeyObjectUtil
    {

        SecretVaultDBContext _context = new SecretVaultDBContext();

        public PrivacyStatus getPrivacyStatus(string privacyString)
        {
            return _context.PrivacyStatuses.Where(privacy => privacy.Status.ToLower().Equals(privacyString.ToLower())).ToList().First();
        }
    }
}
