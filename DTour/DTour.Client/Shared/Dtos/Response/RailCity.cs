namespace DTour.Client.Shared.Dtos.Response;

public class RailCity
{
    public string? Slug { get; set; }
    public string? CountryCode { get; set; }
    public RailLanguageName? Translations { get; set; }
    public Coordinate? Coordinates { get; set; }
}