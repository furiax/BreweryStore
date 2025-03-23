using BreweryStore.Api.Authorization;
using BreweryStore.Api.Data;
using BreweryStore.Api.Endpoints;
using BreweryStore.Api.ErrorHandling;
using BreweryStore.Api.MiddleWare;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddBreweryStoreAuthorization();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new(1.0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.ConfigureExceptionHandler());
app.UseMiddleware<RequestTimingMiddleware>();

await app.Services.InitializeDbAsync();

app.UseHttpLogging();
app.MapBrewsEndpoints();

app.Run();
