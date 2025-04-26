namespace DTour.Domain.Entities.Data;

public class CustomerRequest: AuditableEntityNew<Guid>, ICustomerRequest
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
}