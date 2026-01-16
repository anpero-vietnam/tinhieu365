using ServiceStack.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ultil.Cache
{
    public static class CacheHelper 
    {
        static ICacheProvider _iCacheProvider;
        
        static ICacheProvider iCacheProvider {
            get
            {
                if (_iCacheProvider == null )
                {
                    if (AEnum.SiteConfig.UsingRedisCache)
                    {
                        _iCacheProvider = new RedisCacheProvider();

                    }
                    else
                    {
                        _iCacheProvider = new MemCacheProvider();
                    }
                }
                return _iCacheProvider;
            }
        }
       
       public static void Set<T>(string cacheKey, T objects)
        {
            try
            {
                iCacheProvider.Set<T>(cacheKey,objects);
            }
            catch (Exception)
            {
            }
        }
        public static void Set<T>(string cacheKey, T objects, double cacheMinutes)
        {
            try
            {

                iCacheProvider.Set<T>(cacheKey,objects, cacheMinutes);
            }
            catch (Exception)
            {

            }
        }
        public static T Get<T>(string cacheKey)
        {

            try
            {
                return iCacheProvider.Get<T>(cacheKey);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        public static bool TryGet<T>(string cacheKey, out T outPut)
        {

            try
            {
              return  iCacheProvider.TryGet(cacheKey, out outPut);
        
            }
            catch (Exception)
            {
                outPut = default(T);
                return false;
            }
        }
        public static async Task<T> TryGetAsync<T>(string cacheKey)
        {

            try
            {
                T rs = default(T);
                return rs= await  iCacheProvider.TryGetAsync<T>(cacheKey);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        public static bool Remove(string cacheKey)
        {
            try
            {
                iCacheProvider.Remove(cacheKey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static bool ResetAllCache()
        {
            try
            {
                iCacheProvider.ResetAllCache();
                return true;
            }
            catch
            { }

            return false;
        }
    }


}
