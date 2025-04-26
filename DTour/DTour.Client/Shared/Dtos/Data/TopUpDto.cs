namespace DTour.Client.Shared.Dtos.Data;

public class TopUpDto : AggregateRoot<Guid>, ITopUpDto
{
    public double RequestAmount { get; set; }
    public DateTime RequestDate { get; set; } = DateTime.Today.ToUniversalTime();
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
    public string? FileContent { get; set; }
    public string? FileName { get; set; }
    public bool IsDeposit { get; set; }
}

public interface ITopUpDto
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
    public string? FileContent { get; set; }
    public string? FileName { get; set; }
    public bool IsDeposit { get; set; }
}