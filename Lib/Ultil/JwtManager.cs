using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ultil
{
    public class JwtManagers
    {
        private static string Secret = ToBase64(@"ssdfsadfasdfa22342sdfrgetyeryerty###T#%asdf234rfasdfasdfasd23r1q234sdfsadfasd24@$@$234234__(lsdfas"); //ConfigurationManager.AppSettings["JwtSecret"];
        public static string GenerateToken(string username, string userId,string Surname, string avatar,string address,string Credit,int expireMinutes = 20)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier,userId),
                    new Claim(ClaimTypes.Surname, Surname??string.Empty),                    
                    new Claim("address", address??string.Empty),
                    new Claim("avata", avatar??string.Empty),
                    new Claim("Credit", Credit??"0")
                     
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

        //public static bool ValidateToken(string token)
        //{
        //    username = null;

        //    var simplePrinciple = GetClaimsPrincipal(token);
        //    var identity = simplePrinciple.Identity as ClaimsIdentity;

        //    if (identity == null)
        //        return false;
            
        //    if (!identity.IsAuthenticated)
        //        return false;

        //    var usernameClaim = identity.FindFirst(ClaimTypes.Name);
        //    var NameIdentifier = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    var Surname = identity.FindFirst(ClaimTypes.Surname)?.Value;
        //    var Agen = identity.FindFirst("roleList")?.Value;
        //    username = usernameClaim?.Value;

        //    if (string.IsNullOrEmpty(username))
        //        return false;
        //    // More validate to check whether username exists in system
        //    return true;
        //}
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
