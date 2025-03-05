using BreweryStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreweryStore.Api.Data;

public class BreweryStoreContext : DbContext
{
    public BreweryStoreContext(DbContextOptions<BreweryStoreContext> options)
        : base(options)
    {
    }

    public DbSet<Brew> Brews => Set<Brew>();
}