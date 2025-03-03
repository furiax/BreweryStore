using BreweryStore.Api.Endpoints;
using BreweryStore.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IBrewsRepository, InMemBrewsRepository>();
var app = builder.Build();

app.MapBrewsEndpoints();

app.Run();
