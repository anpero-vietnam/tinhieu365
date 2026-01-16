using System.Security.Cryptography;
using System.Text;


namespace Ultil
{
    public class MD5Helper
    {
        public static string GetMD5Hash(string rawString)
        {
            var encode = new UnicodeEncoding();
            byte[] passwordBytes = encode.GetBytes(rawString);
            byte[] hash;

            var md5 = new MD5CryptoServiceProvider();
            hash = md5.ComputeHash(passwordBytes);

            var sb = new StringBuilder();
            foreach (byte outputByte in hash)
            {
                sb.Append(outputByte.ToString("x2").ToUpper());
            }

            return sb.ToString();
        }
    }
}
