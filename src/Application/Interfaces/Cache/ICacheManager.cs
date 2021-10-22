using System;
using System.Threading.Tasks;

namespace Application.Interfaces.Cache
{
    public interface ICacheManager
    {
        Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, int? cacheTime = null);

        Task SetAsync(string key, object data, int cacheTime);

        Task<bool> IsSetAsync(string key);

        Task<T> GetAsync<T>(string key);

        Task<object> GetAsync(string key);

        Task<object> GetAsync(string key, Type type);

        Task<bool> RemoveAsync(string key);
    }
}