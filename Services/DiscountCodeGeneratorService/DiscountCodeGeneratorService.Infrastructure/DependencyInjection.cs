using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountCodeGeneratorService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<Persistence.DiscountDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<Domain.Interfaces.IDiscountCodeRepository, Repositories.DiscountCodeRepository>();

        //using (var scope = services.CreateScope())
        //{
        //    var context = scope.ServiceProvider.GetRequiredService<Persistence.DiscountDbContext>();

        //    // Apply any pending migrations
        //    if (context.Database.GetPendingMigrations().Any())
        //    {
        //        context.Database.Migrate();
        //    }
        //}

        return services;
    }
}
