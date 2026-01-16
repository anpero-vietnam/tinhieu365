using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ultil
{
    public class CookieHelper
    {
        /// <summary>
        /// Set cookie value specified by the key.
        /// </summary>
        public static void SetCookie(string key, string value, int minutes)
        {
            HttpCookie cookie = new HttpCookie(key);
            cookie.Value = value;
            cookie.Path = "/";
            cookie.Expires = DateTime.Now.AddMinutes(minutes);
            if (string.IsNullOrEmpty(GetCookie(key))){
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
        }

        public static void SetCookie(string key, string value)
        {
            SetCookie(key, value, 360);
        }

        /// <summary>
        /// Get cookie value specified by the key.
        /// </summary>
        public static string GetCookie(string key)
        {
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                return HttpContext.Current.Request.Cookies[key].Value;
            }
            else
                return null;
        }

        /// <summary>
        /// Remove cookie specified by the key.
        /// </summary>
        public static void RemoveCookie(string key)
        {
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                HttpContext.Current.Response.Cookies[key].Expires = DateTime.Now.AddDays(-1);
            }
        }

        public static void RemoveAllCookies()
        {
            foreach (string key in HttpContext.Current.Request.Cookies.AllKeys)
            {
                HttpContext.Current.Response.Cookies[key].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }
}
