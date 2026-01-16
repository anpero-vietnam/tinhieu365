using AEnum;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultil.Cache
{
    //Install-Package ServiceStack.Redis -Version 5.5.0
    internal class RedisCacheProvider : ICacheProvider
    {
        RedisEndpoint _endPoint;
        public RedisCacheProvider()
        {
            _endPoint = new RedisEndpoint(SiteConfig.RedisIP,SiteConfig.RedisPort,SiteConfig.RedisPass);            
        }
        public void Set<T>(string key, T value)
        {
            this.Set(key, value,0);
            
        }
        /// <summary>
        /// set redis cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minuteTimeout">minuteTimeout is set max value</param>
        public void Set<T>(string key, T value, double minuteTimeout)
        {
            TimeSpan timeOut = TimeSpan.FromMinutes(minuteTimeout);
            if (minuteTimeout == 0)
            {
                timeOut = TimeSpan.MaxValue;
            }
            else
            {
                timeOut = TimeSpan.FromMinutes(minuteTimeout);
            }
            using (RedisClient client = new RedisClient(_endPoint))
            {
                client.Set(key, value, timeOut);                    
                //client.As<T>().SetValue(key, value, TimeSpan.FromMinutes(minuteTimeout));
            }
        }
        public T Get<T>(string key)
        {
            T result = default(T);

            using (RedisClient client = new RedisClient(_endPoint))
            {
                var wrapper = client.As<T>();

                result = wrapper.GetValue(key);
            }
            return result;
        }
       public async Task<T> TryGetAsync<T>(string cacheKey)
        {

            T result = default(T);
            try
            {
                
                var cacheTask = Task.Run(()=> {
                
                    using (RedisClient client = new RedisClient(_endPoint))
                    {

                        var wrapper = client.As<T>();                        
                        result = wrapper.GetValue(cacheKey);
                    }
                });
                await cacheTask;
            }
            catch (Exception)
            {

            }            
            return result; 
        }
        public bool TryGet<T>(string key, out T outPut)
        {
            T result = default(T);
            try
            {
                using (RedisClient client = new RedisClient(_endPoint))
                {
                    
                    var wrapper = client.As<T>();
                    result = wrapper.GetValue(key);
                }
                
            }
            catch (Exception)
            {
                
            }
            outPut = result == null ? default(T) : result;
            return result == null ? false : true;
        }
      
        public bool Remove(string key)
        {
            bool removed = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                removed = client.Remove(key);
            }

            return removed;
        }
        public  bool ResetAllCache()
        {
            try
            {
                using (RedisClient client = new RedisClient(_endPoint))
                {
                    var keyList= client.GetAllKeys();
                    if (keyList != null)
                    {
                        foreach (var item in keyList)
                        {
                            client.Remove(item);
                        }
                    }
                }
                return true;
            }
            catch
            {

            }

            return false;
        }
        //public bool IsInCache(string key)
        //{
        //    bool isInCache = false;

        //    using (RedisClient client = new RedisClient(_endPoint))
        //    {
        //        isInCache = client.ContainsKey(key);
        //    }

        //    return isInCache;
        //}
    }
}
