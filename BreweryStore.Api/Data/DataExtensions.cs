using BreweryStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BreweryStore.Api.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BreweryStoreContext>();
        await dbContext.Database.MigrateAsync();

        var logger = serviceProvider.GetRequiredService<ILoggerFactory>()
                                    .CreateLogger("DB Initializer");
        logger.LogInformation(5, "The database is ready!");
    }
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("BreweryStoreContext");
        services.AddSqlServer<BreweryStoreContext>(connString)
                .AddScoped<IBrewsRepository, EntitiyFrameworkBrewsRepository>();
        return services;
    }
}