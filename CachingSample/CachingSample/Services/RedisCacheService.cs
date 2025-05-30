using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CachingSample.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _redis;

        public RedisCacheService(IDistributedCache redis)
        {
            _redis = redis;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var json = await _redis.GetStringAsync(key);
            return json is null ? default : JsonSerializer.Deserialize<T>(json);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expire = null)
        {
            var json = JsonSerializer.Serialize(value);
            await _redis.SetStringAsync(key, json, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expire ?? TimeSpan.FromMinutes(5)
            });
        }

        public Task RemoveAsync(string key) => _redis.RemoveAsync(key);
    }

}
