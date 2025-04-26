using DTour.Application.Features.Data.Commands;
using DTour.Application.Features.Data.Queries;

namespace DTour.Controllers.Data;
public class OtherController : AnonymousApiController
{
    [HttpPost]
    public async Task<IActionResult> Post(AddEditDataRequest<CustomerRequestDto> request)
    {
        return Ok(await _mediator!.Send(new AddEditCustomerRequestCommand()
        {
            Request = request
        }));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator!.Send(new GetAllPostQuery()
        {
            KindPost = id
        });
        return Ok(result);
    }
}