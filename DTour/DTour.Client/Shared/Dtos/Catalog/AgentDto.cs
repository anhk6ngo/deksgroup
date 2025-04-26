namespace DTour.Client.Shared.Dtos.Catalog;

public class AgentDto:AggregateRoot<Guid>, ICAgent
{
    public string? FileContent { get; set; }
    public string? FileName { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? ContactPerson { get; set; }
    public string? Phone { get; set; }
    public string? TaxNo { get; set; }
    public string? BankAccount { get; set; }
    public string? BankName { get; set; }
    public DateTime? SignContract { get; set; }
}

public interface ICAgent
{
    public string? FileContent { get; set; }
    public string? FileName { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? ContactPerson { get; set; }
    public string? Phone { get; set; }
    public string? TaxNo { get; set; }
    public string? BankAccount { get; set; }
    public string? BankName { get; set; }
    public DateTime? SignContract { get; set; }
}