using Microsoft.Extensions.Options;
using SharedExtension.PaymentGateway;

namespace DTour.Infrastructure.Services;

public class VnPayService(IOptions<VnPayConfig> vnPayConfig) : IVnPayService
{
    private readonly VnPayConfig _vnPayConfig = vnPayConfig.Value;

    public string PayNow(MakePayNow data)
    {
        var vnpCom = new vnpRequestPayCommand
        {
            vnp_ReturnUrl = $"{data.Path}",
            vnp_TmnCode = $"{_vnPayConfig.TmnCode}",
            vnp_TxnRef = $"{data.Id}",
            vnp_Amount = $"{data.Amount * 100}",
            vnp_OrderInfo = $"Thanh toan hoa don so: {data.Id}",
            vnp_OrderType = $"{_vnPayConfig.OrderType}"
        };
        return VnPayLibrary.CreateRequestUrl(vnpCom, $"{_vnPayConfig.Url}", $"{_vnPayConfig.HashSecret}");
    }

    public bool ValidateSignature(string secureHash, vnpResponsePayCommand data)
    {
        return VnPayLibrary.ValidateSignature(secureHash, $"{_vnPayConfig.HashSecret}", data);
    }
}