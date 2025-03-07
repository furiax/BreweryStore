using BreweryStore.Api.Data;
using BreweryStore.Api.Endpoints;
using BreweryStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

await app.Services.InitializeDbAsync();

app.MapBrewsEndpoints();

app.Run();
