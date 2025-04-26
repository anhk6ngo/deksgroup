namespace DTour.Application.Features.Data.Commands;

public class DeletePostCommand : IRequest<ResultClient<Guid>>
{
    public Guid Id { get; set; }
}

internal class DeletePostCommandHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<DeletePostCommand, ResultClient<Guid>>
{
    public async Task<ResultClient<Guid>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var currentItem = await unitOfWork.RepositoryNew<Post>().GetByIdAsync(request.Id);
        if (currentItem == null) return await ResultClient<Guid>.FailAsync("Not found the item");
        currentItem.IsActive = false;
        await unitOfWork.RepositoryNew<Post>().UpdateAsync(currentItem);
        await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllPostCacheKey);
        return await ResultClient<Guid>.SuccessAsync(currentItem.Id, "The item deleted");
    }
}