namespace DTour.Client.Shared.Dtos.Response;

public class RailCancelBookingResponse: RailConfirm
{
    public string? BookingSessionStatus { get; set; }
    public string? ReservationExpirationTime { get; set; }
    public string? BookingExpirationTime { get; set; }
    public List<RailCancelTicket>? Tickets { get; set; }
}