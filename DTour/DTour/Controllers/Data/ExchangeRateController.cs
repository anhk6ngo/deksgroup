using DTour.Application.Features.Data.Commands;
using DTour.Application.Features.Data.Queries;
using Microsoft.AspNetCore.Authorization;

namespace DTour.Controllers.Data;
[Authorize(Roles = RoleConstants.AccountingRole)]
public class ExchangeRateController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var results = await _mediator!.Send(new GetAllExchangeRateQuery());
        return Ok(results);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddEditDataRequest<ExchangeRateDto> request)
    {
        return Ok(await _mediator!.Send(new AddEditExchangeRateCommand()
        {
            Request = request
        }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await _mediator!.Send(new DeleteExchangeRateCommand() { Id = id }));
    }
}