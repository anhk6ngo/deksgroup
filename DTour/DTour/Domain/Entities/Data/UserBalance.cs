namespace DTour.Domain.Entities.Data;

public class UserBalance: AggregateRoot<Guid>, IUserBalance
{
    public double Amount { get; set; }
    public double DepositAmount { get; set; }
}