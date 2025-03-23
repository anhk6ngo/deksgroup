namespace DTour.Domain.Entities.Catalog;

public class CExchangeRate : AuditableEntityNew<Guid>, IExchangeRate
{
    public string? Currency { get; set; }
    public double Rate { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
}