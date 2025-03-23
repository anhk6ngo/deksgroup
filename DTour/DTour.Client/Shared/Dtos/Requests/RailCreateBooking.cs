namespace DTour.Client.Shared.Dtos.Requests;

public class RailCreateBooking: RailConfirm
{
    public List<string>? SelectedOutbound { get; set; }
    public List<string>? SelectedInbound { get; set; }
    public List<RailPassenger>? Passengers { get; set; }
    public RailBuyer? Buyer { get; set; }
    public double? Amount { get; set; } = 0;
    public StoreBookingDto? Data { get; set; } = new ();
}