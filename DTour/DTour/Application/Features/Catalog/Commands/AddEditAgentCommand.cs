using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace DTour.Application.Features.Catalog.Commands;

public class AddEditAgentCommand : IRequest<Result<Guid>>
{
    public AddEditDataRequest<AgentDto> Request { get; set; } = default!;
}

internal class AddEditAgentCommandHandler(
    IUnitOfWork<Guid, PortalContext> unitOfWork,
    UserManager<CustomUser> userManager)
    : IRequestHandler<AddEditAgentCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddEditAgentCommand command, CancellationToken cancellationToken)
    {
        switch (command.Request.Action)
        {
            case ActionCommandType.Add:
                var oNewItem = command.Request.Data.Adapt<CAgent>();
                await unitOfWork.RepositoryNew<CAgent>().AddAsync(oNewItem);
                await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllAgentCacheKey);
                await UpdateAgentId(oNewItem.Id, $"{oNewItem.Email}");
                return await Result<Guid>.SuccessAsync(oNewItem.Id, "The item added");
            default:
                var currentItem = await unitOfWork.RepositoryNew<CAgent>().GetByIdAsync(command.Request.Data!.Id);
                if (currentItem != null)
                {
                    var sOld = currentItem.Email;
                    command.Request.Data.Adapt(currentItem);
                    await unitOfWork.RepositoryNew<CAgent>().UpdateAsync(currentItem);
                    await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllAgentCacheKey);
                    await UpdateAgentId(currentItem.Id, $"{currentItem.Email}", $"{sOld}");
                    return await Result<Guid>.SuccessAsync(currentItem.Id, "The item updated");
                }

                break;
        }

        return await Result<Guid>.FailAsync("Not found the item");
    }

    private async Task UpdateAgentId(Guid id, string email, string remove = "")
    {
        var aAddEmail = email.SplitExt();
        var aRemoveEmail = remove.SplitExt();
        if (aRemoveEmail is { Length: > 0 })
        {
            foreach (var s in aRemoveEmail)
            {
                var user = await userManager.FindByEmailAsync(s);
                if (user == null) continue;
                var claims = await userManager.GetClaimsAsync(user);
                var oFindClaim = claims.FirstOrDefault(w => w.Type == "AgentId");
                if (oFindClaim == null) continue;
                await userManager.RemoveClaimAsync(user, oFindClaim);
            }
        }

        foreach (var s in aAddEmail)
        {
            var user = await userManager.FindByEmailAsync(s);
            if (user == null) continue;
            await userManager.AddClaimAsync(user, new Claim("AgentId", $"{id}"));
        }
    }
}