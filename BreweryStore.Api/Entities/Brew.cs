namespace BreweryStore.Api.Entities;

public class Brew
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; }
    public decimal Price { get; set; }
    public required decimal BottleSize { get; set; }
    public required decimal AlchoholPercentage { get; set; }
    public required string BreweryName { get; set; }
    public required string ImageUri { get; set; }
}