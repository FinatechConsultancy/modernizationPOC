using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Etx.Infrastructure.Cache.Distributed
{
    public class RedisCache : IDistributedCache
    {
        private readonly IDistributedCache _cache;

        public RedisCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            var value = _cache.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };
            var serializedValue = JsonSerializer.Serialize(value);
            _cache.SetString(key, serializedValue, options);
        }

        public byte[]? Get(string key)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]?> GetAsync(string key, CancellationToken token = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Refresh(string key)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(string key, CancellationToken token = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public Task RemoveAsync(string key, CancellationToken token = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options,
            CancellationToken token = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}