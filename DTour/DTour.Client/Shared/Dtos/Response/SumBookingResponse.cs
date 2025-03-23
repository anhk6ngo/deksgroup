namespace DTour.Client.Shared.Dtos.Response;

public class SumBookingResponse
{
    public int ActiveNo { get; set; }
    public int TicketType { get; set; }
    public int DeActiveNo { get; set; }
    public int PaymentType { get; set; }
    public double Amount { get; set; }
    public DateTime TransDate { get; set; }
}