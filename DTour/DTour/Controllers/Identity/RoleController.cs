using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Enums;

namespace DTour.Controllers.Identity;

[Authorize(Roles = RoleConstants.AdministratorRole)]
public class RoleController(IRoleService roleService) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var lstRole = await roleService.GetAllAsync();
        var tmp = lstRole.Select(s => new RoleUserDto()
        {
            Description = s.Description,
            id = s.Id,
            Name = s.Name,
            Users = s.UserRoles!.Count
        }).ToList();
        return Ok(tmp);
    }

    [HttpGet("By")]
    public async Task<IActionResult> GetBy([FromQuery] string? id, [FromQuery] EFindRole action,
        [FromQuery] string? claimType, [FromQuery] string? claimValue)
    {
        var lstRole = await roleService.GetByAsync($"{id}", action, $"{claimType}", $"{claimType}");
        return Ok(lstRole.Adapt<List<RoleUserDto>>());
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddEditRoleRequest request)
    {
        if (request.IsSetRight)
        {
            var result =
                await roleService.SetRightsAsync(
                    request.Action == ActionCommandType.Copy ? request.Data!.Description : request.Data!.Name!,
                    request.Data!.id!, request.Action);
            return Ok(result);
        }

        var item = request.Data.Adapt<CustomRole>();
        return Ok(await roleService.AddEditRoleAsync(item, request.Action));
    }

    [HttpPost("lock-account")]
    [Authorize(Roles = RoleConstants.AdministratorRole)]
    public async Task<IActionResult> LockAccount(AddEditRoleRequest request)
    {
        return Ok(await roleService.LockAccountAsync($"{request.Data?.id}", request.Action));
    }
}