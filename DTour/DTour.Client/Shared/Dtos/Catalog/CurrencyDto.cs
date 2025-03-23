namespace DTour.Client.Shared.Dtos.Catalog;

public class CurrencyDto : AggregateRoot<Guid>, ICurrencyDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}

public interface ICurrencyDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}