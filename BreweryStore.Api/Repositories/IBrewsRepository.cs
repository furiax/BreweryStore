using BreweryStore.Api.Entities;

namespace BreweryStore.Api.Repositories;

public interface IBrewsRepository
{
    void Create(Brew brew);
    void Delete(int id);
    Brew? Get(int id);
    IEnumerable<Brew> GetAll();
    void Update(Brew updatedBrew);
}
