using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ultil.Cache
{
    internal class MemCacheProvider : ICacheProvider
    {

        public void Set<T>(string cacheKey, T objects)
        {
            try
            {

                System.Web.HttpRuntime.Cache.Insert(cacheKey, objects, null, DateTime.MaxValue, TimeSpan.Zero);

            }
            catch (Exception)
            {

            }
        }
        public void Set<T>(string cacheKey, T objects, double cacheMinutes)
        {
            try
            {
                System.Web.HttpRuntime.Cache.Insert(cacheKey, objects, null, DateTime.Now.AddMinutes(cacheMinutes), TimeSpan.Zero);

            }
            catch (Exception)
            {

            }
        }
        public T Get<T>(string cacheKey)
        {

            try
            {
                return (T)System.Web.HttpRuntime.Cache[cacheKey];
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        public bool TryGet<T>(string cacheKey, out T outPut)
        {

            try
            {
                if (System.Web.HttpRuntime.Cache[cacheKey] != null)
                {
                    outPut = (T)System.Web.HttpRuntime.Cache[cacheKey];
                    return true;
                }
                else
                {
                    outPut = default(T);
                    return false;
                }

            }
            catch (Exception)
            {
                outPut = default(T);
                return false;
            }
        }
        public async Task<T> TryGetAsync<T>(string cacheKey)
        {
            var outPut = default(T);
            var thisTask = Task.Run(()=> {
                if (System.Web.HttpRuntime.Cache[cacheKey] != null)
                {
                    outPut = (T)System.Web.HttpRuntime.Cache[cacheKey];
                }
                else
                {
                    outPut = default(T);
                }
            });
            await thisTask;
            return outPut;
        }
        public bool Remove(string cacheKey)
        {
            try
            {
                System.Web.HttpRuntime.Cache.Remove(cacheKey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ResetAllCache()
        {
            try
            {
                if (HttpRuntime.Cache == null) return false;
                var caches = HttpRuntime.Cache;
                foreach (DictionaryEntry cache in caches)
                {
                    caches.Remove(cache.Key.ToString());
                }
                return true;
            }
            catch
            { }

            return false;
        }
    }
}
