using BreweryStore.Api.Data;
using BreweryStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreweryStore.Api.Repositories;

public class EntitiyFrameworkBrewsRepository : IBrewsRepository
{
    private readonly BreweryStoreContext dbContext;
    private readonly ILogger<EntitiyFrameworkBrewsRepository> logger;

    public EntitiyFrameworkBrewsRepository(BreweryStoreContext dbContext, ILogger<EntitiyFrameworkBrewsRepository> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<IEnumerable<Brew>> GetAllAsync()
    {
        throw new InvalidOperationException("The database connection is closed");
        return await dbContext.Brews.AsNoTracking().ToListAsync();
    }

    public async Task<Brew?> GetAsync(int id)
    {
        throw new InvalidOperationException("The database connection is closed");
        return await dbContext.Brews.FindAsync(id);
    }

    public async Task CreateAsync(Brew brew)
    {
        dbContext.Brews.Add(brew);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Created brew {Name} with price {Price}.", brew.Name, brew.Price);
    }

    public async Task UpdateAsync(Brew updatedBrew)
    {
        dbContext.Update(updatedBrew);
        await dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        await dbContext.Brews.Where(brew => brew.Id == id)
                    .ExecuteDeleteAsync();
    }

}
