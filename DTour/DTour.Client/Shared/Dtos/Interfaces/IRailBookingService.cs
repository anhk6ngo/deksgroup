
namespace DTour.Client.Shared.Dtos.Interfaces;

public interface IRailBookingService: IManager
{
    public Task<RailResult<List<RailSearchResponse>>> Search(RailSearch request);
    public Task<RailResult<List<RailAmenityList>>> GetAmenity(RailAmenityRequest request);
    public Task<RailResult<List<RailOfferPriceResponse>>> OfferPrice(RailOfferPrice request);
    public Task<RailResult<List<RailOfferValidateResponse>>> OfferValidate(RailOfferValidate request);
    public Task<RailResult<List<RailCreateBookingResponse>>> CreateBooking(RailCreateBooking request);
    public Task<RailResult<List<RailConfirmBookingResponse>>> BookingConfirm(RailConfirm request);
    public Task<RailResult<List<RailConfirmBookingResponse>>> RetrieveBooking(string? bookingSessionId);
    public Task<RailResult<List<RailCancelBookingResponse>>> CancellationBookingDetails(string? bookingSessionId);
    public Task<RailResult<List<RailCancelBookingSessionResponse>>> CancelBooking(string? bookingSessionId);
    public Task<RailResult<List<RailLocationOnline>>> GetStation(string input);
    public Task SendEmail(SendEmailRequest request);
}