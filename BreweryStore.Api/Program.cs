using BreweryStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapBrewsEndpoints();

app.Run();
