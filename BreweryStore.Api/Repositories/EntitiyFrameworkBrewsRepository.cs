using BreweryStore.Api.Data;
using BreweryStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreweryStore.Api.Repositories;

public class EntitiyFrameworkBrewsRepository : IBrewsRepository
{
    private readonly BreweryStoreContext dbContext;

    public EntitiyFrameworkBrewsRepository(BreweryStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Brew>> GetAllAsync()
    {
        return await dbContext.Brews.AsNoTracking().ToListAsync();
    }

    public async Task<Brew?> GetAsync(int id)
    {
        return await dbContext.Brews.FindAsync(id);
    }

    public async Task CreateAsync(Brew brew)
    {
        dbContext.Brews.Add(brew);
        await dbContext.SaveChangesAsync();
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
