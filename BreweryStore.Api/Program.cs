using BreweryStore.Api.Data;
using BreweryStore.Api.Endpoints;
using BreweryStore.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IBrewsRepository, InMemBrewsRepository>();
var connString = builder.Configuration.GetConnectionString("BreweryStoreContext");
builder.Services.AddSqlServer<BreweryStoreContext>(connString);

var app = builder.Build();

app.MapBrewsEndpoints();

app.Run();
