namespace DTour.Client.Shared.Dtos.Requests;

public class RailSearch : RailConfirm
{
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public string? DepartureDate { get; set; }
    public string? DepartureTime { get; set; } = "07:00:00";
    public List<RailPassenger>? Passengers { get; set; }
    public string? Direction { get; set; } = "outbound";
    public string? ReturnDate { get; set; }
    public string? ReturnTime { get; set; }
    public bool ConvertVnd { get; set; } = true;
}