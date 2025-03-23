using System.Diagnostics;
using BreweryStore.Api.Authorization;
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

        group.MapGet("/", async (IBrewsRepository repository, ILoggerFactory loggerFactory) =>
        {
            try
            {
                return Results.Ok((await repository.GetAllAsync()).Select(brew => brew.AsDto()));
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger("Brews Endpoints");
                logger.LogError(ex, "Could not process a request on machine {Machine}. TraceId: {TraceId}", Environment.MachineName, Activity.Current?.TraceId);

                return Results.Problem(
                    title: "We made a mistake but we're working on it!",
                    statusCode: StatusCodes.Status500InternalServerError,
                    extensions: new Dictionary<string, object?>
                    {
                        {"traceId", Activity.Current?.TraceId.ToString()}
                    }
                );
            }

        });

        group.MapGet("/{id}", async (IBrewsRepository repository, int id) =>
        {
            Brew? brew = await repository.GetAsync(id);
            return brew is not null ? Results.Ok(brew.AsDto()) : Results.NotFound();
        })
        .WithName(GetBrewEndPointName)
        .RequireAuthorization(Policies.ReadAccess);

        group.MapPost("/", async (IBrewsRepository repository, CreateBrewDto brewDto) =>
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

            await repository.CreateAsync(brew);
            return Results.CreatedAtRoute(GetBrewEndPointName, new { id = brew.Id }, brew);
        })
        .RequireAuthorization(Policies.WriteAccess);

        group.MapPut("/{id}", async (IBrewsRepository repository, int id, UpdateBrewDto updatedBrewDto) =>
        {
            Brew? existingBrew = await repository.GetAsync(id);

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

            await repository.UpdateAsync(existingBrew);
            return Results.NoContent();
        })
        .RequireAuthorization(Policies.WriteAccess);

        group.MapDelete("/{id}", async (IBrewsRepository repository, int id) =>
        {
            Brew? brew = await repository.GetAsync(id);

            if (brew is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        })
        .RequireAuthorization(Policies.WriteAccess);

        return group;
    }
}