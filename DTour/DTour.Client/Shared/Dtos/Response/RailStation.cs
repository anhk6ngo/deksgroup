namespace DTour.Client.Shared.Dtos.Response;

public class RailStation
{
    public string? Slug { get; set; }
    public string? City { get; set; }
    public string? CountryCode { get; set; }
    public bool MainStation { get; set; }
    public bool AllStations { get; set; }
    public string? Type { get; set; }
    public RailLanguageName? Translations { get; set; }
    public Coordinate? Coordinates { get; set; }
}