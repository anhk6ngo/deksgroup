namespace DTour.Domain.Entities.Data;

public class Commission: AuditableEntityNew<Guid>, ICommission
{
    public string? AgentId { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public double? Percent { get; set; }
    public string? Email { get; set; }
}
