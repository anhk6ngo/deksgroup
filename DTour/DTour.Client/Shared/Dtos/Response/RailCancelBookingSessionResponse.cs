namespace DTour.Client.Shared.Dtos.Response;

public class RailCancelBookingSessionResponse : RailConfirm
{
    public string? BookingSessionStatus { get; set; }
    public string? ReservationExpirationTime { get; set; }
    public string? BookingExpirationTime { get; set; }
    public bool Success { get; set; }
}