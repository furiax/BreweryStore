using BreweryStore.Api.Entities;

const string GetBrewEndPointName = "GetBrew";

List<Brew> brews = new()
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

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var group = app.MapGroup("/brews");

group.MapGet("/", () => brews);

group.MapGet("/{id}", (int id) =>
{
    Brew? brew = brews.Find(brew => brew.Id == id);
    if (brew is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(brew);
})
.WithName(GetBrewEndPointName);

group.MapPost("/", (Brew brew) =>
{
    brew.Id = brews.Max(brew => brew.Id) + 1;
    brews.Add(brew);

    return Results.CreatedAtRoute(GetBrewEndPointName, new { id = brew.Id }, brew);
});

group.MapPut("/{id}", (int id, Brew updatedBrew) =>
{
    Brew? existingBrew = brews.Find(brew => brew.Id == id);

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

    return Results.NoContent();
});

group.MapDelete("/{id}", (int id) =>
{
    Brew? brew = brews.Find(brew => brew.Id == id);

    if (brew is not null)
    {
        brews.Remove(brew);
    }

    return Results.NoContent();
});

app.Run();
