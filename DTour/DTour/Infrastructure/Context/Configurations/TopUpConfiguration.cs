using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTour.Infrastructure.Context.Configurations;

public class TopUpConfiguration : IEntityTypeConfiguration<TopUp>
{
    public void Configure(EntityTypeBuilder<TopUp> builder)
    {
        builder.Property(p => p.UserId).HasMaxLength(36);
        builder.Property(p => p.AccNote).HasMaxLength(500);
        builder.Property(p => p.Note).HasMaxLength(500);
        builder.Property(p => p.DisplayName).HasMaxLength(255);
        builder.Property(p => p.Gateway).HasMaxLength(255);
        builder.Property(p => p.TranId).HasMaxLength(255);
        builder.Property(p => p.FileName).HasMaxLength(255);
        builder.HasIndex(u => new {u.IsActive, u.RequestDate, u.Status, u.UserId});
    }
}