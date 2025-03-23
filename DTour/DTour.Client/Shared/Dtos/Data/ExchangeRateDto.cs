namespace DTour.Client.Shared.Dtos.Data;

public class ExchangeRateDto : AggregateRoot<Guid>, IExchangeRate
{
    public string? Currency { get; set; }
    public double Rate { get; set; }
    public DateTime From { get; set; } = DateTime.Today;
    public DateTime? To { get; set; }
}

public interface IExchangeRate
{
    public string? Currency { get; set; }
    public double Rate { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
}