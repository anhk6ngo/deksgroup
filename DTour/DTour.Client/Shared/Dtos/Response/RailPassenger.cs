namespace DTour.Client.Shared.Dtos.Response;

public class RailPassenger
{
    public int Age { get; set; }
    public string? Id { get; set; }
    public string? Type { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime Dob { get; set; } = DateTime.Today.FirstOfYear();
    public string? BirthDate { get; set; }
    public string? DocumentNumber { get; set; }
    public string? DocumentType { get; set; }
}