namespace DTour.Client.Shared.Dtos.Response;

public class RailStationList
{
    public int Total { get; set; }
    public RailStation[] StationList { get; set; } = default!;
}