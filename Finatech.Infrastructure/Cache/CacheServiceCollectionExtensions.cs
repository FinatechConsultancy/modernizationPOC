
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;


namespace Etx.Infrastructure.Cache
{
    public static class CacheServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationCache(this IServiceCollection services, IConfiguration configuration)
        {
            
            //Add Memory cache by default
            services.AddMemoryCache();
            
            // Retrieve and bind cache settings
            CacheSettings cacheSettings = configuration.GetSection(nameof(CacheSettings)).Get<CacheSettings>() 
                                          ?? throw new ArgumentNullException(nameof(CacheSettings), "Cache settings are not configured.");

            

            if (cacheSettings.CacheType == "Redis")
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    // Configure Redis options here if needed
                });
                services.AddSingleton<IDistributedCache, RedisCache>();
            }
            else if (cacheSettings.CacheType == "NCache")
            {
                // Initialize NCache
                services.AddSingleton<IDistributedCache>(provider =>
                {
                    return new NCacheDistributedCache();
                    
                });
    
            }
            else
            {
                throw new ArgumentException($"Unsupported cache type: {cacheSettings.CacheType}");
            }

            return services;
        }
    }
}