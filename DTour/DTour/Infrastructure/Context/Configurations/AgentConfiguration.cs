using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTour.Infrastructure.Context.Configurations;

public class AgentConfiguration : IEntityTypeConfiguration<CAgent>
{
    public void Configure(EntityTypeBuilder<CAgent> builder)
    {
        builder.Property(p => p.SignContract).HasColumnType("timestamp(6)");
        builder.Property(p => p.Code).HasMaxLength(36);
        builder.Property(p => p.Name).HasMaxLength(255);
        builder.Property(p => p.Email).HasMaxLength(500);
        builder.Property(p => p.ContactPerson).HasMaxLength(255);
        builder.Property(p => p.Phone).HasMaxLength(255);
        builder.Property(p => p.BankAccount).HasMaxLength(36);
        builder.Property(p => p.TaxNo).HasMaxLength(36);
        builder.Property(p => p.BankName).HasMaxLength(255);
    }
}