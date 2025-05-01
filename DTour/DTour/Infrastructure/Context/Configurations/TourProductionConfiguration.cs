using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTour.Infrastructure.Context.Configurations;

public class TourProductionConfiguration : IEntityTypeConfiguration<TourProduction>
{
    public void Configure(EntityTypeBuilder<TourProduction> builder)
    {
        builder.ToTable("tourproduction");
        builder.Property(p => p.DurationDetail).HasMaxLength(255);
        builder.Property(p => p.Schedule).HasMaxLength(255);
        builder.Property(p => p.Size).HasMaxLength(255);
        builder.Property(p => p.Language).HasMaxLength(255);
        builder.Property(p => p.Airlines).HasMaxLength(255);
        builder.Property(p => p.Location).HasMaxLength(255);
        builder.Property(p => p.Departure).HasMaxLength(255);
        builder.Property(p => p.Hotel).HasMaxLength(255);
        builder.Property(p => p.Objective).HasMaxLength(255);
        builder.Property(p => p.Brand).HasMaxLength(255);
        builder.Property(p => p.PriceSymbol).HasMaxLength(1);
        builder.OwnsMany(o => o.ContentDetails, u => { u.ToJson(); });
    }
}