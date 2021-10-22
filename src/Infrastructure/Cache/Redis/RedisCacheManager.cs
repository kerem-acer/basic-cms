using System;
using System.Threading.Tasks;
using Application.Interfaces.Cache;
using Infrastructure.Cache.Constants;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.Cache.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly IConnectionMultiplexer _cache;
        private readonly IDatabase _db;

        public RedisCacheManager(IConnectionMultiplexer cache)
        {
            _cache = cache;
            _db = cache.GetDatabase(CachingDefaults.Database);
        }

        public async Task<T> GetAsync<T>(string key, 
            Func<Task<T>> acquire, 
            int? cacheTime = null)
        {
            if (await IsSetAsync(key))
                return await GetAsync<T>(key);

            var result = await acquire();

            if ((cacheTime ?? CachingDefaults.CacheTime) > 0)
                await SetAsync(key, result, cacheTime ?? CachingDefaults.CacheTime);

            return result;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var item = await _db.StringGetAsync(key);

            return JsonConvert.DeserializeObject<T>(item);
        }

        public async Task<object> GetAsync(string key)
        {
            var item = await _db.StringGetAsync(key);

            return JsonConvert.DeserializeObject(item);
        }

        public async Task<object> GetAsync(string key, Type type)
        {
            var item = await _db.StringGetAsync(key);

            return JsonConvert.DeserializeObject(item, type);
        }

        public async Task<bool> IsSetAsync(string key) =>
            await _db.KeyExistsAsync(key);

        public async Task<bool> RemoveAsync(string key) =>
            await _db.KeyDeleteAsync(key);

        public async Task SetAsync(string key, 
            object data, 
            int cacheTime)
        {
            var dataString = JsonConvert.SerializeObject(data);

            var expiry = TimeSpan.FromMinutes(cacheTime);

            await _db.StringSetAsync(key, dataString, expiry: expiry);
        }
    }
}