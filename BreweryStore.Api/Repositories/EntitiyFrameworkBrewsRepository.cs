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

    public IEnumerable<Brew> GetAll()
    {
        return dbContext.Brews.AsNoTracking().ToList();
    }

    public Brew? Get(int id)
    {
        return dbContext.Brews.Find(id);
    }

    public void Create(Brew brew)
    {
        dbContext.Brews.Add(brew);
        dbContext.SaveChanges();
    }

    public void Update(Brew updatedBrew)
    {
        dbContext.Update(updatedBrew);
        dbContext.SaveChanges();
    }
    public void Delete(int id)
    {
        dbContext.Brews.Where(brew => brew.Id == id)
                       .ExecuteDelete();
    }

}
