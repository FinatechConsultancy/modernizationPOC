using Etx.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Etx.Infrastructure.Utilities;

public class ReflectionFactory
{
    public object? CreateInstance(Type type)
    {
        if (ServiceLocator.ServiceProvider != null)
            return ActivatorUtilities.CreateInstance(ServiceLocator.ServiceProvider, type);
        return null;
    }
}