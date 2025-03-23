namespace DTour.Application.Features.Data.Commands;

public class DeleteCommissionCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}

internal class DeleteCommissionCommandHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<DeleteCommissionCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteCommissionCommand request, CancellationToken cancellationToken)
    {
        var currentItem = await unitOfWork.RepositoryNew<Commission>().GetByIdAsync(request.Id);
        if (currentItem == null) return await Result<Guid>.FailAsync("Not found the item");
        currentItem.IsActive = false;
        await unitOfWork.RepositoryNew<Commission>().UpdateAsync(currentItem);
        await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllCommissionCacheKey);
        return await Result<Guid>.SuccessAsync(currentItem.Id, "The item deleted");
    }
}