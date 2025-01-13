using Microsoft.Extensions.DependencyInjection;

namespace Etx.Infrastructure.Service;

public static class ServiceLocator
{
    public static IServiceProvider? ServiceProvider { get; set; }

    public static T? GetService<T>()
    {
        return ServiceProvider!.GetService<T>();
    }
}