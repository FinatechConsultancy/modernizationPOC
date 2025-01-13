using Finatech.AccountManagement.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Finatech.AccountManagement.Extensions;

public static class AccountManagementServiceExtensions
{
    public static IServiceCollection AddAccountManagementModule(this IServiceCollection services)
    {
        services.AddScoped<AccountBusiness>();
        return services;
    }
}