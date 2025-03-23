namespace DTour.Client.Shared.Dtos.Requests;

public class BalanceRequest
{
    public string? UserId { get; set; }
    public double? Amount { get; set; }
    public ActionCommandType Action { get; set; } = ActionCommandType.GetData;
}