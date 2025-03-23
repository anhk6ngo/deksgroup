namespace DTour.Client.Shared.Dtos.Requests;

public class GetReportRequest
{
    public string? DateRange { get; set; }
    public List<int> Status { get; set; } = [0];
    public string? UserId { get; set; }
    public string? BookingCode { get; set; }
    public int Operator { get; set; } = 0;
    public ActionCommandType CommandType { get; set; } = ActionCommandType.GetData;
}