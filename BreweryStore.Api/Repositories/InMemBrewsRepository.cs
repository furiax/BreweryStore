using BreweryStore.Api.Entities;

namespace BreweryStore.Api.Repositories;

public class InMemBrewsRepository : IBrewsRepository
{
    private readonly List<Brew> brews = new()
    {
        new Brew()
        {
            Id= 1,
            Name = "Jupiler",
            Category = "Pils",
            Price = 0.80M,
            BottleSize = 0.25M,
            AlchoholPercentage = 5.2M,
            BreweryName = "InBev",
            ImageUri = "https://placehold.co/100",
        },
            new Brew()
        {
            Id= 2,
            Name = "Duvel",
            Category = "Sterk Blond Bier",
            Price = 1.62M,
            BottleSize = 0.33M,
            AlchoholPercentage = 8.5M,
            BreweryName = "Duvel-Moortgat",
            ImageUri = "https://placehold.co/100",
        },
            new Brew()
        {
            Id= 3,
            Name = "Lindemans Kriek",
            Category = "Fruit",
            Price = 1.34M,
            BottleSize = 0.25M,
            AlchoholPercentage = 3.5M,
            BreweryName = "Lindemans",
            ImageUri = "https://placehold.co/100",
        }
    };

    public async Task<IEnumerable<Brew>> GetAllAsync()
    {
        return await Task.FromResult(brews);
    }

    public async Task<Brew?> GetAsync(int id)
    {
        return await Task.FromResult(brews.Find(brew => brew.Id == id));
    }

    public async Task CreateAsync(Brew brew)
    {
        brew.Id = brews.Max(brew => brew.Id) + 1;
        brews.Add(brew);

        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Brew updatedBrew)
    {
        var index = brews.FindIndex(brew => brew.Id == updatedBrew.Id);
        brews[index] = updatedBrew;

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var index = brews.FindIndex(brew => brew.Id == id);
        brews.RemoveAt(index);

        await Task.CompletedTask;
    }
}