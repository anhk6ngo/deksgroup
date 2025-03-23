namespace DTour.Client.Shared.Dtos.Requests;

public class RailAmenityRequest
{
    public string? Language { get; set; } = "en";
    public List<string>? Ids { get; set; }
}