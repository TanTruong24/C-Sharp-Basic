using Microsoft.Extensions.Caching.Memory;

namespace CachingSample.Services
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memory;

        public InMemoryCacheService(IMemoryCache memory)
        {
            _memory = memory;
        }

        public Task<T?> GetAsync<T>(string key)
        {
            _memory.TryGetValue(key, out T? value);
            return Task.FromResult(value);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expire = null)
        {
            _memory.Set(key, value, expire ?? TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            _memory.Remove(key);
            return Task.CompletedTask;
        }
    }

}
