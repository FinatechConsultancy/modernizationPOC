using System.Reflection;
using System.Reflection.Metadata;
using Finatech.Infrastructure.Cache;
using Finatech.Infrastructure.Attributes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Finatech.Infrastructure.Decorators;

public class CachingDecorator<T> : DispatchProxy
{
    private T _decorated;
    private IMemoryCache _memoryCache;
    private IDistributedCache _distributedCache;

    public void SetParameters(T decorated, IMemoryCache memoryCache, IDistributedCache distributedCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        var cacheableAttribute = _decorated.GetType().GetMethod(targetMethod.Name).GetCustomAttribute<CacheableAttribute>();
        var invalidateAttributes = _decorated.GetType().GetMethod(targetMethod.Name).GetCustomAttributes<InvalidateCacheableAttribute>();
        var cacheKey = String.Empty;
        if (cacheableAttribute != null)
        {
            cacheKey = generateCacheKey(_decorated, targetMethod, cacheableAttribute.CacheKeyPrefix, cacheableAttribute.CacheKey, args);
            
            if (cacheableAttribute.CacheType == CacheType.Memory)
            {
                if (_memoryCache.TryGetValue(cacheKey, out var cachedValue))
                    return cachedValue;

                var result = targetMethod.Invoke(_decorated, args);
                _memoryCache.Set(cacheKey, result, TimeSpan.FromSeconds(cacheableAttribute.ExpirationInSeconds));
                return result;
            }
            else if (cacheableAttribute.CacheType == CacheType.Distributed)
            {
                var cachedValue = _distributedCache.GetString(cacheKey);
                if (cachedValue != null)
                    return JsonConvert.DeserializeObject(cachedValue, targetMethod.ReturnType);

                var result = targetMethod.Invoke(_decorated, args);
                _distributedCache.SetString(cacheKey, JsonConvert.SerializeObject(result), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheableAttribute.ExpirationInSeconds)
                });
                return result;
            }
        }
        
        //call target method
        var methodResult = targetMethod.Invoke(_decorated, args);

        // Handle multiple InvalidateCacheable attributes
        foreach (var invalidateAttribute in invalidateAttributes)
        {
            cacheKey = generateCacheKey(_decorated, targetMethod, invalidateAttribute.CacheKeyPrefix, invalidateAttribute.CacheKey, args);
            if (invalidateAttribute.CacheType == CacheType.Memory)
            {
                _memoryCache.Remove(cacheKey);
            }
            else
            {
                _distributedCache.Remove(cacheKey);
            }
        }
        
        return methodResult;
    }
    

    private string generateCacheKey(T decoreated, MethodInfo targetMethod, string cacheKeyPrefix, string cacheKey, object[] args)
    {
        if(string.IsNullOrEmpty(cacheKeyPrefix)) 
            cacheKeyPrefix = $"{_decorated.GetType().Name}.{targetMethod.Name}";


        //var parameter = targetMethod.GetParameters().Where(p => cacheKey.StartsWith(p.Name)).FirstOrDefault();
        var parameters  = targetMethod.GetParameters();
        for (int i = 0; i < args.Length; i++)
        {
            if(cacheKey.StartsWith(parameters[i].Name))
                cacheKey = ExpressionEvaluator.EvaluateExpression(cacheKey, args[i]).ToString(); break;
        }
        
        
        
        
        return $"{cacheKeyPrefix}.{cacheKey}";
    }
    
}
