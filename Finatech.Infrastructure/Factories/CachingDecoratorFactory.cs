using System.Reflection;
using Finatech.Infrastructure.Decorators;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Finatech.Infrastructure.Factories;

public static class CachingDecoratorFactory
{
    public static object Create(Type serviceType, object serviceImplementation, IMemoryCache memoryCache, IDistributedCache distributedCache)
    {
        var proxy = DispatchProxy.Create(serviceType, typeof(CachingDecorator<>).MakeGenericType(serviceType));
        var setParametersMethod = proxy.GetType().GetMethod("SetParameters");
        setParametersMethod.Invoke(proxy, new object[] { serviceImplementation, memoryCache, distributedCache });
        return proxy;
    }
}