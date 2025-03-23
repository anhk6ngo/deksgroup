namespace DTour.Client.Shared.Dtos.Response;

public class RailResult<T> where T : class
{
    public int Status { get; set; }
    public T? Data { get; set; }
    public List<RailError>? Errors { get; set; }
    public string? Id { get; set; }
    public List<string>? Urls { get; set; }
}