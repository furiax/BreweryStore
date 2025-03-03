using System.ComponentModel.DataAnnotations;

namespace BreweryStore.Api.Entities;

public class Brew
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    [Required]
    [StringLength(50)]
    public required string Category { get; set; }
    [Required]
    [Range(1, 100)]
    public decimal Price { get; set; }
    [Required]
    [Range(0.05, 5)]
    public required decimal BottleSize { get; set; }
    [Required]
    [Range(0, 99)]
    public required decimal AlchoholPercentage { get; set; }
    [Required]
    [StringLength(50)]
    public required string BreweryName { get; set; }
    [Required]
    [Url]
    [StringLength(100)]
    public required string ImageUri { get; set; }
}