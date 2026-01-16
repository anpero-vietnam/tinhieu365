using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultil
{
    public interface ICacheProvider
    {
        void Set<T>(string key, T value);
        bool TryGet<T>(string cacheKey, out T outPut);
        Task<T> TryGetAsync<T>(string cacheKey);
        void Set<T>(string key, T value, double minuteTimeout);
        T Get<T>(string key);
        bool ResetAllCache();
        bool Remove(string cacheKey);
        // bool IsInCache(string key);
    }
}
