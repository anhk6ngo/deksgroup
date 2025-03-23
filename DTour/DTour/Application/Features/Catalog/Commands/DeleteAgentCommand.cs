namespace DTour.Application.Features.Catalog.Commands;

public class DeleteAgentCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}

internal class DeleteAgentCommandHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<DeleteAgentCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteAgentCommand request, CancellationToken cancellationToken)
    {
        var currentItem = await unitOfWork.RepositoryNew<CAgent>().GetByIdAsync(request.Id);
        if (currentItem == null) return await Result<Guid>.FailAsync("Not found the item");
        currentItem.IsActive = false;
        await unitOfWork.RepositoryNew<CAgent>().UpdateAsync(currentItem);
        await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllAgentCacheKey);
        return await Result<Guid>.SuccessAsync(currentItem.Id, "The item deleted");
    }
}