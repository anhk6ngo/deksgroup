namespace DTour.Client.Shared.Dtos.Response;

public class RailCreateBookingResponse: RailConfirm
{
    public string? BookingSessionStatus { get; set; }
    public string? ReservationExpirationTime { get; set; }
    public string? BookingExpirationTime { get; set; }
    public bool Success { get; set; }
    public double TotalPrice { get; set; }
    public List<RailTicket>? Tickets { get; set; }
}