using Finatech.Infrastructure.Cache;

namespace Finatech.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class CacheableAttribute : Attribute
{
    public string CacheKey { get; }
    public int ExpirationInSeconds { get; }
    public CacheType CacheType { get; }
    public string CacheKeyPrefix { get; }

    public CacheableAttribute(string cacheKey, int expirationInSeconds, CacheType cacheType = CacheType.Memory, string cacheKeyPrefix = "")
    {
        CacheKey = cacheKey;
        ExpirationInSeconds = expirationInSeconds;
        CacheType = cacheType;
        CacheKeyPrefix = cacheKeyPrefix;
    }
}
