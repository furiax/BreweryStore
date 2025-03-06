using BreweryStore.Api.Data;
using BreweryStore.Api.Endpoints;
using BreweryStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IBrewsRepository, InMemBrewsRepository>();
var connString = builder.Configuration.GetConnectionString("BreweryStoreContext");
builder.Services.AddSqlServer<BreweryStoreContext>(connString);

var app = builder.Build();

app.Services.InitializeDb();

app.MapBrewsEndpoints();

app.Run();
