using BreweryStore.Api.Authorization;
using BreweryStore.Api.Data;
using BreweryStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddBreweryStoreAuthorization();

var app = builder.Build();

await app.Services.InitializeDbAsync();

app.UseHttpLogging();
app.MapBrewsEndpoints();

app.Run();
