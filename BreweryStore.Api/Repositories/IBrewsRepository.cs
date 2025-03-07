using BreweryStore.Api.Entities;

namespace BreweryStore.Api.Repositories;

public interface IBrewsRepository
{
    Task CreateAsync(Brew brew);
    Task DeleteAsync(int id);
    Task<Brew?> GetAsync(int id);
    Task<IEnumerable<Brew>> GetAllAsync();
    Task UpdateAsync(Brew updatedBrew);
}
