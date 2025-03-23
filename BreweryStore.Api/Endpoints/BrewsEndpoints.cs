using System.Diagnostics;
using BreweryStore.Api.Authorization;
using BreweryStore.Api.Dtos;
using BreweryStore.Api.Entities;
using BreweryStore.Api.Repositories;

namespace BreweryStore.Api.Endpoints;

public static class BrewsEndpoints
{
    const string GetBrewV1EndPointName = "GetBrewV1";
    const string GetBrewV2EndPointName = "GetBrewV2";


    public static RouteGroupBuilder MapBrewsEndpoints(this IEndpointRouteBuilder routes)
    {
        var V1Group = routes.MapGroup("/v1/brews")
                .WithParameterValidation();

        var V2Group = routes.MapGroup("/v2/brews")
                .WithParameterValidation();

        V1Group.MapGet("/", async (IBrewsRepository repository, ILoggerFactory loggerFactory) =>
        {
            return Results.Ok((await repository.GetAllAsync()).Select(brew => brew.AsDtoV1()));
        });

        V1Group.MapGet("/{id}", async (IBrewsRepository repository, int id) =>
        {
            Brew? brew = await repository.GetAsync(id);
            return brew is not null ? Results.Ok(brew.AsDtoV1()) : Results.NotFound();
        })
        .WithName(GetBrewV1EndPointName)
        .RequireAuthorization(Policies.ReadAccess);

        //V2 GET ENDPOINTS
        V2Group.MapGet("/", async (IBrewsRepository repository, ILoggerFactory loggerFactory) =>
        {
            return Results.Ok((await repository.GetAllAsync()).Select(brew => brew.AsDtoV2()));
        });
        //V2 GET ENDPOINTS
        V2Group.MapGet("/{id}", async (IBrewsRepository repository, int id) =>
        {
            Brew? brew = await repository.GetAsync(id);
            return brew is not null ? Results.Ok(brew.AsDtoV2()) : Results.NotFound();
        })
        .WithName(GetBrewV2EndPointName)
        .RequireAuthorization(Policies.ReadAccess);

        V1Group.MapPost("/", async (IBrewsRepository repository, CreateBrewDto brewDto) =>
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
            return Results.CreatedAtRoute(GetBrewV1EndPointName, new { id = brew.Id }, brew);
        })
        .RequireAuthorization(Policies.WriteAccess);

        V1Group.MapPut("/{id}", async (IBrewsRepository repository, int id, UpdateBrewDto updatedBrewDto) =>
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

        V1Group.MapDelete("/{id}", async (IBrewsRepository repository, int id) =>
        {
            Brew? brew = await repository.GetAsync(id);

            if (brew is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        })
        .RequireAuthorization(Policies.WriteAccess);

        return V1Group;
    }
}