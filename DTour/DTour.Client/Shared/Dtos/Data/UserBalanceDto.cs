namespace DTour.Client.Shared.Dtos.Data;

public class UserBalanceDto : AggregateRoot<Guid>, IUserBalance
{
    public double Amount { get; set; } = 0;
    public double DepositAmount { get; set; } = 0;
}

public interface IUserBalance
{
    public double Amount { get; set; }
    public double DepositAmount { get; set; }
}