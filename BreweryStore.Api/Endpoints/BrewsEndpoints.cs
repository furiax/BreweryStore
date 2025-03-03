using BreweryStore.Api.Entities;
using BreweryStore.Api.Repositories;

namespace BreweryStore.Api.Endpoints;

public static class BrewsEndpoints
{
    const string GetBrewEndPointName = "GetBrew";

    public static RouteGroupBuilder MapBrewsEndpoints(this IEndpointRouteBuilder routes)
    {
        InMemBrewsRepository repository = new();

        var group = routes.MapGroup("/brews")
                .WithParameterValidation();

        group.MapGet("/", () => repository.GetAll());

        group.MapGet("/{id}", (int id) =>
        {
            Brew? brew = repository.Get(id);
            return brew is not null ? Results.Ok(brew) : Results.NotFound();
        })
        .WithName(GetBrewEndPointName);

        group.MapPost("/", (Brew brew) =>
        {
            repository.Create(brew);
            return Results.CreatedAtRoute(GetBrewEndPointName, new { id = brew.Id }, brew);
        });

        group.MapPut("/{id}", (int id, Brew updatedBrew) =>
        {
            Brew? existingBrew = repository.Get(id);

            if (existingBrew is null)
            {
                return Results.NotFound();
            }

            existingBrew.Name = updatedBrew.Name;
            existingBrew.Category = updatedBrew.Category;
            existingBrew.Price = updatedBrew.Price;
            existingBrew.BottleSize = updatedBrew.BottleSize;
            existingBrew.AlchoholPercentage = updatedBrew.AlchoholPercentage;
            existingBrew.BreweryName = updatedBrew.BreweryName;
            existingBrew.ImageUri = updatedBrew.ImageUri;

            repository.Update(existingBrew);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
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