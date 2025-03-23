namespace DTour.Client.Shared.Dtos.Response;

public class RailTicket
{
    public string? Direction { get; set; }
    public object? SegmentId { get; set; }
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public string? TransportNumber { get; set; }
    public string? TransportType { get; set; }
    public string? ClassName { get; set; }
    public string? DepartureDate { get; set; }
    public string? DepartureTime { get; set; }
    public string? ArrivalDate { get; set; }
    public string? ArrivalTime { get; set; }
    public string? Pnr { get; set; }
    public string? TransportProvider { get; set; }
    public string? TariffName { get; set; }
    public string? Coach { get; set; }
    public string? Seat { get; set; }
    public object? Price { get; set; }
    public List<RailPassenger>? Passengers { get; set; }
    public string? Type { get; set; }
}