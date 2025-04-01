namespace DTour.Domain.Entities.Data;

public class StoreBooking: AuditableEntityNew<Guid>, IStoreBooking
{
    public double Amount { get; set; }
    public DateTime DepartDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public double CommitAmount { get; set; }
    public string? UserId { get; set; }
    public string? BookingCode { get; set; }
    public string? BookingSessionId { get; set; }
    public int Status { get; set; }
    public string? DisplayName { get; set; }
    public int TicketType { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
    public int Adt { get; set; }
    public int Chd { get; set; }
    public int Inf { get; set; }
    public int PaymentType { get; set; }
    public string? TransactionId { get; set; }
    public string? ApiProviderId { get; set; }
    public string? SaveObject { get; set; }
    public string? TicketPdf { get; set; }
    public string? Pnr { get; set; }
    public double? TranFee { get; set; }
    public double? SystemFee { get; set; }
    public double? ServiceFee { get; set; }
    public double? ManagementFee { get; set; }
    public double? ExcRate { get; set; }
    public string? ToEmail { get; set; }
    public double? Price { get; set; }
}