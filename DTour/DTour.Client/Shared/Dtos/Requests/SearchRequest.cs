namespace DTour.Client.Shared.Dtos.Requests;

public class SearchRequest
{
    public bool IsRoundTrip { get; set; }
    public string? Dep { get; set; }
    public string? DepCity { get; set; }
    public string? SlugDep { get; set; }
    public string? DateRange { get; set; }
    public string? Arr { get; set; }
    public string? ArrCity { get; set; }
    public string? SlugArr { get; set; }
    public int Adult { get; set; } = 1;
    public int Child { get; set; } = 0;
    public bool Direct { get; set; } = false;
    public List<int> PaxAge { get; set; } = [];
}