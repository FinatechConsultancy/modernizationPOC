using Finatech.CreditManagement.Business;
using Finatech.CreditManagement.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Finatech.CreditManagement.Extensions;

public static class CreditManagementServiceExtensions
{
    public static IServiceCollection AddCreditModule(this IServiceCollection services)
    {
        services.AddTransient<ICreditService, CreditService>();
        services.AddScoped<CreditBusiness>();
        
        return services;
    }
}