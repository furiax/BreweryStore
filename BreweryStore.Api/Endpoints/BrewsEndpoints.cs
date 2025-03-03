using BreweryStore.Api.Dtos;
using BreweryStore.Api.Entities;
using BreweryStore.Api.Repositories;

namespace BreweryStore.Api.Endpoints;

public static class BrewsEndpoints
{
    const string GetBrewEndPointName = "GetBrew";

    public static RouteGroupBuilder MapBrewsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/brews")
                .WithParameterValidation();

        group.MapGet("/", (IBrewsRepository repository) =>
            repository.GetAll().Select(brew => brew.AsDto()));

        group.MapGet("/{id}", (IBrewsRepository repository, int id) =>
        {
            Brew? brew = repository.Get(id);
            return brew is not null ? Results.Ok(brew.AsDto()) : Results.NotFound();
        })
        .WithName(GetBrewEndPointName);

        group.MapPost("/", (IBrewsRepository repository, CreateBrewDto brewDto) =>
        {
            Brew brew = new()
            {
                Name = brewDto.Name,
                Category = brewDto.Category,
                Price = brewDto.Price,
                BottleSize = brewDto.BottleSize,
                AlchoholPercentage = brewDto.AlchoholPercentage,
                BreweryName = brewDto.BreweryName,
                ImageUri = brewDto.ImageUri
            };

            repository.Create(brew);
            return Results.CreatedAtRoute(GetBrewEndPointName, new { id = brew.Id }, brew);
        });

        group.MapPut("/{id}", (IBrewsRepository repository, int id, UpdateBrewDto updatedBrewDto) =>
        {
            Brew? existingBrew = repository.Get(id);

            if (existingBrew is null)
            {
                return Results.NotFound();
            }

            existingBrew.Name = updatedBrewDto.Name;
            existingBrew.Category = updatedBrewDto.Category;
            existingBrew.Price = updatedBrewDto.Price;
            existingBrew.BottleSize = updatedBrewDto.BottleSize;
            existingBrew.AlchoholPercentage = updatedBrewDto.AlchoholPercentage;
            existingBrew.BreweryName = updatedBrewDto.BreweryName;
            existingBrew.ImageUri = updatedBrewDto.ImageUri;

            repository.Update(existingBrew);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (IBrewsRepository repository, int id) =>
        {
            Brew? brew = repository.Get(id);

            if (brew is not null)
            {
                repository.Delete(id);
            }

            return Results.NoContent();
        });

        return group;
    }
}