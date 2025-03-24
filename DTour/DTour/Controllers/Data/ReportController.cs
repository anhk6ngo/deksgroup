using DTour.Application.Features.Data.Queries;

namespace DTour.Controllers.Data;

public class ReportController : BaseApiController
{
    [HttpPost("sum-by")]
    public async Task<IActionResult> SumByUser(GetReportRequest input)
    {
        input.UserId = User.GetUserId();
        var result = await _mediator!.Send(new GetSumBookingByUserQuery()
        {
            Input = input
        });
        return Ok(result);
    }
    public async Task<IActionResult> GetStoreBooking(GetReportRequest input)
    {
        input.UserId = User.GetUserId();
        if (User.IsInRole(RoleConstants.OperationRole))
        {
            input.UserId = string.Empty;
        }
        var result = await _mediator!.Send(new GetStoreBookingByUserQuery()
        {
            Input = input
        });
        return Ok(result);
    }
}