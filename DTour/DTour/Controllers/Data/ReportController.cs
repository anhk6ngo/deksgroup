using DTour.Application.Features.Data.Queries;

namespace DTour.Controllers.Data;

public class ReportController : BaseApiController
{
    [HttpPost("sum-by")]
    public async Task<IActionResult> SumByUser(GetReportRequest input)
    {
        input.UserId = User.GetUserId();
        if (User.IsInRole(RoleConstants.AgentAccountingRole))
        {
            input.UserId = User.GetClaimByName("AgentId");
            input.Operator = 1;
        }
        else if (User.IsInRole(RoleConstants.AccountingRole))
        {
            input.UserId = string.Empty;
        }

        var result = await _mediator!.Send(new GetSumBookingByUserQuery()
        {
            Input = input
        });
        return Ok(result);
    }

    public async Task<IActionResult> GetStoreBooking(GetReportRequest input)
    {
        input.UserId = User.GetUserId();
        if (User.IsInRole(RoleConstants.AgentAccountingRole))
        {
            input.UserId = User.GetClaimByName("AgentId");
            input.Operator = 1;
        }
        else if (User.IsInRole(RoleConstants.OperationRole) || User.IsInRole(RoleConstants.AccountingRole))
        {
            input.UserId = string.Empty;
        }

        var result = await _mediator!.Send(new GetStoreBookingByUserQuery()
        {
            Input = input
        });
        return Ok(result);
    }

    [HttpPost("cash-flow")]
    public async Task<IActionResult> CashFlowReport(GetReportRequest input)
    {
        if (!User.IsInRole(RoleConstants.AccountingRole))
        {
            input.UserId = User.GetClaimByName("AgentId");
        }
        var result = await _mediator!.Send(new GetCashFlowByUserQuery()
        {
            Input = input
        });
        return Ok(result);
    }
}