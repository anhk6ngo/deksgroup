namespace DTour.Client.Shared.Dtos.Data;

public class TourProductionDto: AggregateRoot<Guid>, ITourProductionDto
{
    public int TourGroup { get; set; } = 0;
    public bool Inbound { get; set; } = false;
    public int Duration { get; set; } = 1;
    public string? DurationDetail { get; set; }
    public string? Schedule { get; set; }
    public string? Size { get; set; }
    public string? Language { get; set; } = "Tiếng Việt";
    public string? Airlines { get; set; }
    public string? Location { get; set; }
    public string? Departure { get; set; }
    public string? Hotel { get; set; }
    public double? Price { get; set; } = 0;
    public string? PriceSymbol { get; set; } = "đ";
    public string? Objective { get; set; }
    public string? Brand { get; set; }
    public DateTime? PublicDate { get; set; }
    public List<ContentDetail>? ContentDetails { get; set; } = [];
}

public interface ITourProductionDto
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

public class ContentDetail
{
    //0 - Tour Info, 1 - Include, 2 - Not Include, 3 - Media, 4 - Help
    public int Group { get; set; } = 0;
    public string? Title { get; set; }
    public string? Description { get; set; }
}