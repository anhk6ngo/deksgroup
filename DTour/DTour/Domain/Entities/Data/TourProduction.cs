namespace DTour.Domain.Entities.Data;

public class TourProduction: AuditableEntityNew<Guid>, ITourProductionDto
{
    public int TourGroup { get; set; }
    public bool Inbound { get; set; }
    public int Duration { get; set; }
    public string? DurationDetail { get; set; }
    public string? Schedule { get; set; }
    public string? Size { get; set; }
    public string? Language { get; set; }
    public string? Airlines { get; set; }
    public string? Location { get; set; }
    public string? Departure { get; set; }
    public string? Hotel { get; set; }
    public double? Price { get; set; }
    public string? PriceSymbol { get; set; }
    public string? Objective { get; set; }
    public string? Brand { get; set; }
    public DateTime? PublicDate { get; set; }
    public List<ContentDetail>? ContentDetails { get; set; }
}