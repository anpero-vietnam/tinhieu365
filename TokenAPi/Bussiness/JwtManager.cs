using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace TokenAPi.Bussiness
{
    public class JwtManager
    {
        private static string Secret = ToBase64(@"ssdfsadfasdfa22342sdfasdf234rfasdfasdfasd23r1q234sdfsadfasd24@$@$234234__(lsdfas"); //ConfigurationManager.AppSettings["JwtSecret"];
        public static string GenerateToken(string username,string userId,string Surname, int expireMinutes = 20)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier,"1"),                    
                    new Claim(ClaimTypes.Surname, Surname),
                    new Claim("Agen", Surname)
                }),
                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public static bool ValidateToken(string token, out string username)
        {
            username = null;

            var simplePrinciple = JwtManager.GetClaimsPrincipal(token);
            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            var NameIdentifier = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Surname = identity.FindFirst(ClaimTypes.Surname)?.Value;
            var Agen = identity.FindFirst("Agen")?.Value;
            username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
                return false;
            // More validate to check whether username exists in system
            return true;
        }
        public static ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch (Exception)
            {
                //should write log
                return null;
            }
        }
        public static string ToBase64(string s)
        {
            byte[] buffer = System.Text.Encoding.Unicode.GetBytes(s);
            return System.Convert.ToBase64String(buffer);
        }
    }
}