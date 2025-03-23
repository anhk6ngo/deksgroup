namespace DTour.Client.Shared.Dtos.Data;

public class CommissionDto: AggregateRoot<Guid>, ICommission
{
    public string? AgentId { get; set; }
    public DateTime From { get; set; } = DateTime.Today;
    public DateTime? To { get; set; }
    public double? Percent { get; set; }
    public string? Email { get; set; }
}

public interface ICommission
{
    public string? AgentId { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public double? Percent { get; set; }
    public string? Email { get; set; }
}