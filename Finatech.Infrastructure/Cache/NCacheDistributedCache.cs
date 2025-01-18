using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;


namespace Finatech.Infrastructure.Cache;

public class NCacheDistributedCache : IDistributedCache
{
    
    // just for simplicity, we are using IMemoryCache here
    
    private readonly IMemoryCache _cache;
    public NCacheDistributedCache(IMemoryCache cache)
    {
        _cache = cache;
    }
    
    public byte[]? Get(string key)
    {
        return _cache.Get<byte[]>(key);
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
        _cache.Set(key, value);
    }

    public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options,
        CancellationToken token = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}