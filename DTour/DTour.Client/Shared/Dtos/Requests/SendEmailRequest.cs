namespace DTour.Client.Shared.Dtos.Requests;

public class SendEmailRequest
{
    public string? BookingCode { get; set; }
    public string? Email { get; set; }
    public List<string>? Urls { get; set; } = [];
}