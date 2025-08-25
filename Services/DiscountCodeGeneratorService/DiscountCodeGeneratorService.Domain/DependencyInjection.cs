using DiscountCodeGeneratorService.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountCodeGeneratorService.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IDiscountCodeService, Services.DiscountCodeService>();
        
        return services;
    }
}
