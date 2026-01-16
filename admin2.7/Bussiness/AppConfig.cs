using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Bussiness
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
        public static string GoogleClientSecret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["GoogleClientSecrecLogin"];
            }
        }        
        //static string master_API_Link;
        //public static string Master_API_Link
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(master_API_Link))
        //        {
        //            master_API_Link = System.Configuration.ConfigurationManager.AppSettings["MASTER_API_LINK"];
        //        }
        //        return master_API_Link;
        //    }
        //}
    }
}