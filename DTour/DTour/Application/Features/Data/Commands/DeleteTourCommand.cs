namespace DTour.Application.Features.Data.Commands;

public class DeleteTourCommand : IRequest<ResultClient<Guid>>
{
    public Guid Id { get; set; }
}

internal class DeleteTourCommandHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<DeleteTourCommand, ResultClient<Guid>>
{
    public async Task<ResultClient<Guid>> Handle(DeleteTourCommand request, CancellationToken cancellationToken)
    {
        var currentItem = await unitOfWork.RepositoryNew<TourProduction>().GetByIdAsync(request.Id);
        if (currentItem == null) return await ResultClient<Guid>.FailAsync("Not found the item");
        currentItem.IsActive = false;
        await unitOfWork.RepositoryNew<TourProduction>().UpdateAsync(currentItem);
        await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllTourCacheKey);
        return await ResultClient<Guid>.SuccessAsync(currentItem.Id, "The item deleted");
    }
}