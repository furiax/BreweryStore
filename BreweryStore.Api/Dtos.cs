using System.ComponentModel.DataAnnotations;

namespace BreweryStore.Api.Dtos;

public record BrewDtoV1(
    int Id,
    string Name,
    string Category,
    decimal Price,
    string ImageUri
);
public record BrewDtoV2(
    int Id,
    string Name,
    string Category,
    decimal Price,
    decimal RetailPrice,
    string ImageUri
);

public record CreateBrewDto(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(50)] string Category,
    [Required][Range(1, 100)] decimal Price,
    [Required][Range(0.05, 5)] decimal BottleSize,
    [Required][Range(0, 99)] decimal AlchoholPercentage,
    [Required][StringLength(50)] string BreweryName,
    [Required][Url][StringLength(100)] string ImageUri
);

public record UpdateBrewDto(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(50)] string Category,
    [Required][Range(1, 100)] decimal Price,
    [Required][Range(0.05, 5)] decimal BottleSize,
    [Required][Range(0, 99)] decimal AlchoholPercentage,
    [Required][StringLength(50)] string BreweryName,
    [Required][Url][StringLength(100)] string ImageUri
);