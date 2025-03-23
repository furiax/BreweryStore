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
        var group = routes.NewVersionedApi()
                          .MapGroup("/brews")
                          .HasApiVersion(1.0)
                          .HasApiVersion(2.0)
                          .WithParameterValidation();

        //V1 GET ENDPOINT
        group.MapGet("/", async (IBrewsRepository repository, ILoggerFactory loggerFactory) =>
        {
            return Results.Ok((await repository.GetAllAsync()).Select(brew => brew.AsDtoV1()));
        }).MapToApiVersion(1.0);
        //V1 GET ENDPOINT
        group.MapGet("/{id}", async (IBrewsRepository repository, int id) =>
        {
            Brew? brew = await repository.GetAsync(id);
            return brew is not null ? Results.Ok(brew.AsDtoV1()) : Results.NotFound();
        })
        .WithName(GetBrewV1EndPointName)
        .RequireAuthorization(Policies.ReadAccess)
        .MapToApiVersion(1.0);

        //V2 GET ENDPOINTS
        group.MapGet("/", async (IBrewsRepository repository, ILoggerFactory loggerFactory) =>
        {
            return Results.Ok((await repository.GetAllAsync()).Select(brew => brew.AsDtoV2()));
        })
        .MapToApiVersion(2.0);
        //V2 GET ENDPOINTS
        group.MapGet("/{id}", async (IBrewsRepository repository, int id) =>
        {
            Brew? brew = await repository.GetAsync(id);
            return brew is not null ? Results.Ok(brew.AsDtoV2()) : Results.NotFound();
        })
        .WithName(GetBrewV2EndPointName)
        .RequireAuthorization(Policies.ReadAccess)
        .MapToApiVersion(2.0);

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
            return Results.CreatedAtRoute(GetBrewV1EndPointName, new { id = brew.Id }, brew);
        })
        .RequireAuthorization(Policies.WriteAccess)
        .MapToApiVersion(1.0);

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
         .RequireAuthorization(Policies.WriteAccess)
         .MapToApiVersion(1.0);

        group.MapDelete("/{id}", async (IBrewsRepository repository, int id) =>
        {
            Brew? brew = await repository.GetAsync(id);

            if (brew is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        })
        .RequireAuthorization(Policies.WriteAccess)
        .MapToApiVersion(1.0);

        return group;
    }
}