using BreweryStore.Api.Dtos;

namespace BreweryStore.Api.Entities;

public static class EntityExtensions
{
    public static BrewDto AsDto(this Brew brew)
    {
        return new BrewDto(
            brew.Id,
            brew.Name,
            brew.Category,
            brew.Price,
            brew.ImageUri
        );
    }
}