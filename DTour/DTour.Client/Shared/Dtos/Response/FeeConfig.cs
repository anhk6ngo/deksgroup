namespace DTour.Client.Shared.Dtos.Response;

public class FeeConfig
{
    public double? BankFee { get; set; } = 0;
    public double? SystemFee { get; set; } = 0;
    public double? ManagementFee { get; set; } = 0;
    public double? ServiceFee { get; set; } = 0;
}