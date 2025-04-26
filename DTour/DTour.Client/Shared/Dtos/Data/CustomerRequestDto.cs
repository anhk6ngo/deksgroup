namespace DTour.Client.Shared.Dtos.Data;

public class CustomerRequestDto: AggregateRoot<Guid>, ICustomerRequest
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
}

public interface ICustomerRequest
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
}