using Microsoft.EntityFrameworkCore;

namespace BreweryStore.Api.Data;

public static class DataExtensions
{
    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BreweryStoreContext>();
        dbContext.Database.Migrate();
    }
}