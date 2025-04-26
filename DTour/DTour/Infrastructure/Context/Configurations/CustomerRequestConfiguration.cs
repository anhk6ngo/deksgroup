using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTour.Infrastructure.Context.Configurations;

public class CustomerRequestConfiguration : IEntityTypeConfiguration<CustomerRequest>
{
    public void Configure(EntityTypeBuilder<CustomerRequest> builder)
    {
        builder.Property(p => p.FullName).HasMaxLength(255);
        builder.Property(p => p.Email).HasMaxLength(255);
        builder.Property(p => p.Title).HasMaxLength(255);
        builder.Property(p => p.Message).HasMaxLength(2000);
    }
}