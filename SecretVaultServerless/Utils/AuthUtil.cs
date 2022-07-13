using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace SecretVaultAPI.Utils
{
    public class AuthUtil
    {

        public JwtSecurityToken decodeJWT(string authHeader)
        {
            string bear = "Bearer ";
            authHeader = authHeader.Substring(bear.Length);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = jsonToken as JwtSecurityToken;
            return tokenS;
        }

        public string getUserName(JwtSecurityToken token)
        {
            return token.Claims.First(claim => claim.Type == "cognito:username").Value;
        }

        public bool isVerified(JwtSecurityToken token)
        {
            string verify = token.Claims.First(claim => claim.Type == "email_verified").Value;
            return true ? verify.Equals("true") : false;
        }

        public bool isUser(JwtSecurityToken token, string usernameFromReq)
        {
            bool email = isVerified(token);
            if(string.IsNullOrEmpty(usernameFromReq))
            {
                return email;
            }
            string usernameFromToken = getUserName(token);
            return true ? usernameFromReq.Equals(usernameFromToken) && email : false;
        }
    }
}
