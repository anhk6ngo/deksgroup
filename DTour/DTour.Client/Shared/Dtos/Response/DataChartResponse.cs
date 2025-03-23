namespace DTour.Client.Shared.Dtos.Response;

public class DataChartResponse
{
    public string? Name { get; set; } = "Amount";
    public List<double>? Data { get; set; }
}