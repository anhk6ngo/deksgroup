using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTour.Infrastructure.Context.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<CCurrency>
{
    public void Configure(EntityTypeBuilder<CCurrency> builder)
    {
        builder.Property(p => p.Code).HasMaxLength(3);
        builder.Property(p => p.Name).HasMaxLength(255);
    }
}