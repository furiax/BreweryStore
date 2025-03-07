namespace BreweryStore.Api.Authorization;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddBreweryStoreAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.ReadAccess, builder =>
                builder.RequireClaim("scope", "brews:read"));
            options.AddPolicy(Policies.WriteAccess, builder =>
                builder.RequireClaim("scope", "brews:write")
                    .RequireRole("Admin"));
        });
        return services;
    }
}