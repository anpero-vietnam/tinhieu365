namespace VNPAYMENT_NET_CS.Common
{
   // using log4net;
    using System;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public class Utils
    {
       

        public static string GetIpAddress()
        {
            string str;
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                str = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(str) || (str.ToLower() == "unknown"))
                {
                    str = request.ServerVariables["REMOTE_ADDR"];
                }
            }
            catch (Exception exception)
            {
                str = "Invalid IP:" + exception.Message;
            }
            return str;
        }

        public static string Md5(string strInput)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(strInput);
            byte[] buffer2 = null;
            buffer2 = new MD5CryptoServiceProvider().ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            foreach (byte num in buffer2)
            {
                builder.AppendFormat("{0:x2}", num);
            }
            return builder.ToString();
        }

        public static string ToAscii(string sInput)
        {
            StringBuilder builder = new StringBuilder();
            string input = null;
            input = Regex.Replace(Regex.Replace(input, "Đ|&#273;", "D"), "đ|&#272;", "d").Normalize(NormalizationForm.FormKD);
            foreach (char ch in input)
            {
                if (char.IsWhiteSpace(ch))
                {
                    builder.Append(" ");
                }
                else if (!(((char.GetUnicodeCategory(ch) == UnicodeCategory.NonSpacingMark) || char.IsPunctuation(ch)) || char.IsSymbol(ch)))
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }
    }
}

