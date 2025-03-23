namespace DTour.Client.Shared.Dtos.Response;

public class RailConfirmBookingResponse : RailConfirm
{
    public string? BookingSessionStatus { get; set; }
    public string? ReservationExpirationTime { get; set; }
    public string? BookingExpirationTime { get; set; }
    public List<string>? PdfTickets { get; set; }
    public string? UrlExpirationTime { get; set; }
}