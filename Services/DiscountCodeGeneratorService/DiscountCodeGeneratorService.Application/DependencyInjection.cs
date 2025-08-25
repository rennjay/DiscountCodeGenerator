using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace DiscountCodeGeneratorService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}
