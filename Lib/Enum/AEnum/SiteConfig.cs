using System;
using System.Configuration;
namespace AEnum 
{
    public class SiteConfig
    {
        
        public static int SaveLoginDay
        {
            get
            {
                int defaultValue = 24;
                int.TryParse(ConfigurationManager.AppSettings["saveLoginDay"],out defaultValue);
                return defaultValue;

            }
        }
        public static string MediaAPITokenKey
        {
            get
            {
                return ConfigurationManager.AppSettings["mediaApiToken"];
            }
        }
        
        public static string MasterAPITokenKey
        {
            get
            {
                return ConfigurationManager.AppSettings["MasterAPIToken"];
            }
        }
        public static string MediaEndPointLink
        {
            get
            {
                return ConfigurationManager.AppSettings["mediaEndpoints"];
            }
        }
        public static bool UsingRedisCache
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["UsingRedisCache"] == "1" ? true : false;
                }
                catch (Exception)
                {

                    return false;
                }
               
            }
        }
        public static string RedisIP
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["RedisIP"];
                }
                catch (Exception)
                {

                    return string.Empty;
                }

            }
        }
        
        public static int ShortCacheTime
        {
            get
            {
                try
                {
                    string shortCacheTime = ConfigurationManager.AppSettings["shortCacheTime"];
                    return string.IsNullOrEmpty(shortCacheTime) ? 15 : Convert.ToInt32(shortCacheTime);
                }
                catch (Exception)
                {
                    return 5;
                }
            }
        }
        public static int RedisPort
        {
            get
            {
                try
                {
                    string port = ConfigurationManager.AppSettings["RedisPort"];
                    return string.IsNullOrEmpty(port) ? 0 : Convert.ToInt32(port);
                }
                catch (Exception)
                {
                    return 6379;
                }

            }
        }
        public static string RedisPass
        {
            get
            {
                try
                {
                    string pass = ConfigurationManager.AppSettings["RedisPass"];
                    return string.IsNullOrEmpty(pass) ?null: pass;
                }
                catch (Exception)
                {
                    return null;
                }

            }
        } 
    
    }
}
