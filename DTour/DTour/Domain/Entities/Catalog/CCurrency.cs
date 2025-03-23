

namespace DTour.Domain.Entities.Catalog;

public class CCurrency: AuditableEntityNew<Guid>, ICurrencyDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}