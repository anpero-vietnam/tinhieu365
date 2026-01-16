using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Controllers
{
    public static class AppConfig
    {
        static string api_BackEnd_Link;
        public static string API_BackEnd_Link
        {
            get
            {
                if (string.IsNullOrEmpty(api_BackEnd_Link))
                {
                    api_BackEnd_Link = System.Configuration.ConfigurationManager.AppSettings["API_BackEnd_Link"];
                }
                return api_BackEnd_Link;
            }
        }
        public static string GoogleClientid
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["GoogleClientidLogin"];
            }
        }
        public static string SaveCookieLoginHour
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SaveCookieLoginHour"];
            }
        }
        public static string GoogleClientSecret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["GoogleClientSecrecLogin"];
            }
        }
        public static string googleCapchaSecret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["googleCapchaSecret"];
            }
        }
        public static string googleCapchaSitekey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["googleCapchaSitekey"];
            }
        }
    }
}