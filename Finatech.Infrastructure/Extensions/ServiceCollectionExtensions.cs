using System.Reflection;
using Finatech.Infrastructure.Attributes;
using Finatech.Infrastructure.Factories;
using Finatech.Infrastructure.Model;
using Finatech.Infrastructure.Service;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Finatech.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        //Create servises wiht this ReflectionFactory class and adds it to DI container.
        // CreateInstance() method simulates RouterInternal.ExecuteDLLMethod() in the legacy code.
        services.AddScoped<IDependencyReflectorFactory, DependencyReflectorFactory>();

        //Add ServiceContext
        services.AddScoped<ServiceContext>();
        
        services.AddScoped<IConfigurationParameterService, ConfigurationParameterService>();
        
        return services;
    }
    public static IServiceCollection AddCachingDecorator(this IServiceCollection services)
    {
        var memoryCache = services.BuildServiceProvider().GetRequiredService<IMemoryCache>();
        var distributedCache = services.BuildServiceProvider().GetRequiredService<IDistributedCache>();

        foreach (var service in services.ToList())
        {
            if (service.ImplementationType != null &&
                service.ImplementationType.GetMethods().Any(m => m.GetCustomAttributes<CacheableAttribute>().Any()))
            {
                var serviceType = service.ServiceType;
                var implementationType = service.ImplementationType;

                services.Remove(service);
                services.AddTransient(serviceType, provider =>
                {
                    var serviceInstance = ActivatorUtilities.CreateInstance(provider, implementationType);
                    return CachingDecoratorFactory.Create(serviceType, serviceInstance, memoryCache, distributedCache);
                });
            }
        }

        return services;
    }
}