using BreweryStore.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreweryStore.Api.Data.Configurations;

public class BrewConfiguration : IEntityTypeConfiguration<Brew>
{
    public void Configure(EntityTypeBuilder<Brew> builder)
    {
        builder.Property(brew => brew.Price)
        .HasPrecision(5, 2);
        builder.Property(brew => brew.BottleSize)
        .HasPrecision(3, 2);
        builder.Property(brew => brew.AlchoholPercentage)
        .HasPrecision(4, 2);
    }
}