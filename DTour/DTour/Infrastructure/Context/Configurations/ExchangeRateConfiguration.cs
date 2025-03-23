using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTour.Infrastructure.Context.Configurations;

public class ExchangeRateConfiguration : IEntityTypeConfiguration<CExchangeRate>
{
    public void Configure(EntityTypeBuilder<CExchangeRate> builder)
    {
        builder.Property(p => p.Currency).HasMaxLength(3);
        builder.Property(p => p.From).HasColumnType("timestamp(6)");
        builder.Property(p => p.To).HasColumnType("timestamp(6)");
        builder.HasIndex(u => new {u.IsActive, u.From, u.To});
    }
}