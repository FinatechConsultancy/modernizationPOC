using Finatech.Infrastructure.Cache;

namespace Finatech.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class InvalidateCacheableAttribute : Attribute
{
    public string CacheKey { get; }
    public string CacheKeyPrefix { get; }
    public CacheType CacheType { get; }

    public InvalidateCacheableAttribute(string cacheKey, CacheType cacheType = CacheType.Memory, string cacheKeyPrefix = "")
    {
        CacheKey = cacheKey;
        CacheType = cacheType;
        CacheKeyPrefix = cacheKeyPrefix;
    }
}
