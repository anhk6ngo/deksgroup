namespace DTour.Client.Shared.Dtos.Response;

public class RailAmenity
{
    public string? Id { get; set; }
    public bool IsTariff { get; set; }
    public bool IsClass { get; set; }
    public RailLanguageName? Translations { get; set; }
}