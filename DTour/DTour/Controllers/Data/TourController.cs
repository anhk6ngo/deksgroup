using DTour.Application.Features.Data.Commands;
using DTour.Application.Features.Data.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using SharedKernel.Models;

namespace DTour.Controllers.Data;
[Authorize(Roles = RoleConstants.ContentRole)]
public class TourController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> AddEditPost(AddEditDataRequest<TourProductionDto> input)
    {
        var result = await _mediator!.Send(new AddEditTourCommand()
        {
            Request = input
        });
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPost()
    {
        var result = await _mediator!.Send(new GetAllTourQuery()
        {
            KindTour = -1
        });
        return Ok(result);
    }

    [HttpDelete("delete-data/{id}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var result = await _mediator!.Send(new DeleteTourCommand()
        {
            Id = id
        });
        return Ok(result);
    }
}