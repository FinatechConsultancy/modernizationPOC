using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Etx.Infrastructure.Service;

public class LegacyAccountService
{
    private IMemoryCache? _memoryCache;
    public LegacyAccountService()
    {
        //This is a legacy service that is created using constructor without dependency injection.
        //But still it can use the service locator to get services from DI container.
        _memoryCache = ServiceLocator.ServiceProvider!.GetService<IMemoryCache>()
                       ?? throw new InvalidOperationException("IMemoryCache service is not available.");
        
    }
}