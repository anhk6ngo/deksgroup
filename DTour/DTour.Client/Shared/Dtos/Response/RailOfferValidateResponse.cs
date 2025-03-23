namespace DTour.Client.Shared.Dtos.Response;

public class RailOfferValidateResponse: RailConfirm
{
    public string? BookingSessionStatus { get; set; }
    public string? CeservationExpirationTime { get; set; }
    public string? BookingExpirationTime { get; set; }
    public bool Success { get; set; }
}