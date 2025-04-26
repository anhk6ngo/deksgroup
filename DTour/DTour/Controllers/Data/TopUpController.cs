using DTour.Application.Features.Data.Commands;
using DTour.Application.Features.Data.Queries;
using Microsoft.AspNetCore.Identity.UI.Services;
using SharedKernel.Models;

namespace DTour.Controllers.Data;

public class TopUpController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetBalance()
    {
        var results = await _mediator!.Send(new GetBalanceByUserQuery
        {
            UserId = User.GetClaimByName("AgentId"),
        });
        return Ok(results);
    }

    [HttpPost("add-edit-data")]
    public async Task<IActionResult> AddEditTopUp(AddEditDataRequest<TopUpDto> input)
    {
        if (!User.IsInRole(RoleConstants.AccountingRole) && input.Data!.Status == 2)
        {
            return Ok(await Result<Guid>.FailAsync("Not found the item"));
        }

        if (input.Data!.UserId.IsNullOrEmpty())
        {
            input.Data.UserId = User.GetClaimByName("AgentId");
            input.Data.DisplayName = User.GetClaimByName("DisplayName");
        }

        var result = await _mediator!.Send(new AddEditTopUpCommand()
        {
            Request = input
        });
        return Ok(result);
    }

    [HttpPost("get-all-data")]
    public async Task<IActionResult> GetAllTopUp(GetReportRequest input)
    {
        input.UserId = User.GetUserId();
        var result = await _mediator!.Send(new GetAllTopUpQuery()
        {
            Input = input
        });
        return Ok(result);
    }

    [HttpDelete("delete-data/{id}")]
    public async Task<IActionResult> DeleteTopUp(Guid id)
    {
        var result = await _mediator!.Send(new DeleteTopUpCommand()
        {
            Id = id
        });
        return Ok(result);
    }
}