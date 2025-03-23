namespace DTour.Client.Shared.Dtos.Response;

public class RailTransferBase
{
    public string? DepartureDate { get; set; }
    public string? DepartureTime { get; set; }
    public string? ArrivalDate { get; set; }
    public string? ArrivalTime { get; set; }
    public RailPlace? Origin { get; set; }
    public RailPlace? Destination { get; set; }
    public string? TravelDuration { get; set; }
}