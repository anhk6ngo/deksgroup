namespace DTour.Domain.Entities.Data;

public class TopUp: AuditableEntityNew<Guid>, ITopUpDto
{
    public double RequestAmount { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ApproveDate { get; set; }
    public double ApproveAmount { get; set; }
    public string? UserId { get; set; }
    public string? Note { get; set; }
    public string? AccNote { get; set; }
    public string? ApproveNote { get; set; }
    public int Status { get; set; }
    public string? DisplayName { get; set; }
    public string? TranId { get; set; }
    public string? Gateway { get; set; }
    public bool IsDeposit { get; set; }
}