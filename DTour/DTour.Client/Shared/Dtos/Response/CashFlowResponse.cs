namespace DTour.Client.Shared.Dtos.Response;

public class CashFlowResponse
{
    public DateTime? TranDate { get; set; }
    public string? Pnr { get; set; }
    public double? In { get; set; }
    public double? Out { get; set; }
    public double? Balance { get; set; }
    public int PaymentType { get; set; }
    public string? UserId { get; set; }
}