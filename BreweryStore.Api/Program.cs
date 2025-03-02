using BreweryStore.Api.Entities;

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

app.MapGet("/brews", () => brews);


app.Run();
