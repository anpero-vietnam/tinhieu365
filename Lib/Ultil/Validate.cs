using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;
using System.Web;

namespace Ultil
{
    public static class CheckValid 
    {
   
        //private String toValidImagesTag(String images) {
        //return Regex.Replace(images, @"^\[https?://(?:[a-z\-]+\.)+[a-z]{2,6}(?:/[^/#?]+)+\.(?:jpg|gif|png)\]$", @"^\<https?://(?:[a-z\-]+\.)+[a-z]{2,6}(?:/[^/#?]+)+\.(?:jpg|gif|png)\>$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        //}
        public static Boolean ValidateGoogleCaptcha(string captcha)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + System.Configuration.ConfigurationManager.AppSettings["googleCapchaSecret"] + "&response=" + captcha + "&remoteip=" + HttpContext.Current.Request.UserHostAddress;
            var client = new System.Net.WebClient();
            var GoogleReply = client.DownloadString(url);
            var captchaResponse = JsonConvert.DeserializeObject<ReCaptchaClass>(GoogleReply);
            return Convert.ToBoolean(captchaResponse.Success);
        }


        public static Boolean ValidateCaptcha(string captcha)
        {
            String[] s = (String[])HttpContext.Current.Session["capcha"];

            if (s != null && captcha.Equals(s[1]))
            {
                return true;
            }
            else
            {

                return false;
            }

        }
    
    }
    class ReCaptchaClass
    {

        [JsonProperty("success")]
        public string Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        private string m_Success;
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }


        private List<string> m_ErrorCodes;
    }
}
