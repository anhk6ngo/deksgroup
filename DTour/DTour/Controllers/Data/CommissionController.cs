using DTour.Application.Features.Data.Commands;
using DTour.Application.Features.Data.Queries;
using Microsoft.AspNetCore.Authorization;

namespace DTour.Controllers.Data;
[Authorize(Roles = RoleConstants.AdministratorRole)]
public class CommissionController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var results = await _mediator!.Send(new GetAllCommissionQuery());
        return Ok(results);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddEditDataRequest<CommissionDto> request)
    {
        return Ok(await _mediator!.Send(new AddEditCommissionCommand()
        {
            Request = request
        }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await _mediator!.Send(new DeleteCommissionCommand() { Id = id }));
    }
}