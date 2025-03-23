namespace DTour.Client.Shared.Dtos.Response;

public class RailCancelTicket
{
    public string? Id { get; set; }
    public bool IsCancelable { get; set; }
    public double? RefundAmount { get; set; }
}