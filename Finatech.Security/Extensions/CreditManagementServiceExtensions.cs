using Finatech.Security.Business;
using Finatech.Security.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Finatech.Security.Extensions;

public static class SecurityServiceExtensions
{
    public static IServiceCollection AddSecurityModule(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<UserBusiness>();
        
        return services;
    }
}