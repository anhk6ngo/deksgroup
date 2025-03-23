using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTour.Infrastructure.Context.Configurations;

public class CommissionConfiguration : IEntityTypeConfiguration<Commission>
{
    public void Configure(EntityTypeBuilder<Commission> builder)
    {
        builder.Property(p => p.From).HasColumnType("timestamp(6)");
        builder.Property(p => p.To).HasColumnType("timestamp(6)");
        builder.Property(p => p.AgentId).HasMaxLength(36);
        builder.Property(p => p.Email).HasMaxLength(500);
        builder.HasIndex(u => new {u.IsActive, u.From, u.To});
    }
}