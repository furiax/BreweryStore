using BreweryStore.Api.Entities;

List<Brew> brews = new()
{
    new Brew()
    {
        Id= 1,
        Name = "Jupiler",
        Category = "Pils",
        Price = 0.80M,
        BreweryName = "InBev",
        ImageUri = "https://placehold.co/100",
    }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
