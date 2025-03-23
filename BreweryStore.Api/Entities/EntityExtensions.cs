using BreweryStore.Api.Dtos;

namespace BreweryStore.Api.Entities;

public static class EntityExtensions
{
    public static BrewDtoV1 AsDtoV1(this Brew brew)
    {
        return new BrewDtoV1(
            brew.Id,
            brew.Name,
            brew.Category,
            brew.Price,
            brew.ImageUri
        );
    }
    public static BrewDtoV2 AsDtoV2(this Brew brew)
    {
        return new BrewDtoV2(
            brew.Id,
            brew.Name,
            brew.Category,
            brew.Price - (brew.Price * .3m),
            brew.Price,
            brew.ImageUri
        );
    }
}