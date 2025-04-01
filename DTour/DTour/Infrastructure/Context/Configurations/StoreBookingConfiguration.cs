using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTour.Infrastructure.Context.Configurations;

public class StoreBookingConfiguration : IEntityTypeConfiguration<StoreBooking>
{
    public void Configure(EntityTypeBuilder<StoreBooking> builder)
    {
        builder.Property(p => p.DepartDate).HasColumnType("timestamp(6)");
        builder.Property(p => p.ReturnDate).HasColumnType("timestamp(6)");
        builder.Property(p => p.UserId).HasMaxLength(36);
        builder.Property(p => p.BookingCode).HasMaxLength(36);
        builder.Property(p => p.From).HasMaxLength(255);
        builder.Property(p => p.To).HasMaxLength(255);
        builder.Property(p => p.BookingSessionId).HasMaxLength(255);
        builder.Property(p => p.DisplayName).HasMaxLength(255);
        builder.Property(p => p.TransactionId).HasMaxLength(50);
        builder.Property(p => p.ApiProviderId).HasMaxLength(36);
        builder.Property(p => p.Pnr).HasMaxLength(10);
        builder.Property(p => p.ToEmail).HasMaxLength(255);
        builder.Property(p => p.SaveObject).HasMaxLength(4000);
        builder.Property(p => p.TicketPdf).HasMaxLength(4000);
        builder.HasIndex(u => new {u.IsActive, u.CreatedOn, u.Status, u.TicketType, u.UserId});
        builder.HasIndex(u => new {u.Status, u.TicketType, u.Pnr});
    }
}