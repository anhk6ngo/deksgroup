using DTour.Application.Features.Catalog.Commands;
using DTour.Application.Features.Catalog.Queries;
using Microsoft.AspNetCore.Authorization;

namespace DTour.Controllers.Catalog;
[Authorize(Roles = RoleConstants.AccountingRole)]
public class AgentController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var results = await _mediator!.Send(new GetAllAgentQuery());
        return Ok(results);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddEditDataRequest<AgentDto> request)
    {
        return Ok(await _mediator!.Send(new AddEditAgentCommand()
        {
            Request = request
        }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await _mediator!.Send(new DeleteAgentCommand() { Id = id }));
    }
}