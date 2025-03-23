using SharedExtension.PaymentGateway;

namespace DTour.Controllers.PaymentGateway
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class VnPayController(
        IVnPayService service,
        IUnitOfWork<Guid, PortalContext> context,
        IRailBookingService railBookingService)
        : AnonymousApiController
    {
        [HttpGet]
        [Route("IPN")]
        public async Task<IActionResult> Ipn()
        {
            var result = new VnpReturnContent();
            var qParam = HttpContext.Request.Query;
            if (qParam.Count > 0)
            {
                var payResponse = new vnpResponsePayCommand();
                try
                {
                    #region VnPay Payment GateWay

                    GetValueFromQueryString(payResponse, qParam);
                    var vnp_SecureHash = qParam["vnp_SecureHash"];
                    var checkSignature = service.ValidateSignature($"{vnp_SecureHash}", payResponse);
                    if (checkSignature)
                    {
                        var aAmount = Convert.ToInt64(payResponse.vnp_Amount);
                        aAmount /= 100;
                        var id = $"{payResponse.vnp_TxnRef}".Trim().Replace("_", "-").ToGuid();
                        var oFind = await context.RepositoryNew<StoreBooking>().GetByIdAsync(id);
                        if (oFind == null)
                        {
                            result.RspCode = "01";
                            result.Message = "Order not found";
                        }
                        else
                        {
                            if (Math.Abs(oFind.Amount.ToRound() - aAmount) > 0)
                            {
                                result.RspCode = "04";
                                result.Message = "Invalid Amount";
                            }
                            else if (payResponse.vnp_ResponseCode != "00")
                            {
                                result.RspCode = "00";
                                result.Message = "Confirm Success";
                            }
                            else if (oFind.Status == 2)
                            {
                                result.RspCode = "02";
                                result.Message = "Order already confirmed";
                            }
                            else
                            {
                                //Update Status
                                oFind.Status = 2;
                                await context.RepositoryNew<StoreBooking>().UpdateAsync(oFind);
                                await context.Commit(CancellationToken.None);
                                if (payResponse.vnp_ResponseCode == "00")
                                {
                                    oFind.TransactionId = payResponse.vnp_BankTranNo;
                                    //Confirm Booking
                                    var resultConfirm = await railBookingService.BookingConfirm(new RailConfirm()
                                    {
                                        BookingSessionId = oFind.BookingSessionId,
                                        ApiProviderId = oFind.ApiProviderId
                                    });
                                    if (resultConfirm.Status == 200)
                                    {
                                        var lstLink = new List<string>();
                                        foreach (var item in resultConfirm.Data!.Where(item =>
                                                     item.PdfTickets is { Count: > 0 }))
                                        {
                                            lstLink.AddRange(item.PdfTickets!);
                                        }
                                        var oData = $"{oFind.SaveObject}"
                                            .ToObject<RailResult<List<RailCreateBookingResponse>>?>();
                                        if (oData != null)
                                        {
                                            oData.Urls = lstLink;
                                            oFind.SaveObject = oData.ConvertObjectToString();
                                        }
                                    }
                                    await context.RepositoryNew<StoreBooking>().UpdateAsync(oFind);
                                    await context.Commit(CancellationToken.None);
                                    result.RspCode = "00";
                                    result.Message = "Confirm Success";
                                }
                            }
                        }
                    }
                    else
                    {
                        result.RspCode = "97";
                        result.Message = "Invalid signature";
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    result.RspCode = "99";
                    result.Message = ex.Message;
                }
            }
            else
            {
                result.RspCode = "99";
                result.Message = "Unknown Error";
            }

            return Ok(result);
        }

        private void GetValueFromQueryString(object input, IQueryCollection values)
        {
            if (values.Count == 0) return;
            var list = input.GetType().GetProperties();
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(values[item.Name])) continue;
                var sValue = values[item.Name].ToString();
                item.SetValue(input, sValue);
            }
        }
    }
}