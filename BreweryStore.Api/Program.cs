using BreweryStore.Api.Data;
using BreweryStore.Api.Endpoints;
using BreweryStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

await app.Services.InitializeDbAsync();

app.MapBrewsEndpoints();

app.Run();
