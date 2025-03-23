namespace DTour.Application.Features.Data.Commands;

public class DeleteExchangeRateCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}

internal class DeleteExchangeRateCommandHandler(IUnitOfWork<Guid, PortalContext> unitOfWork, IAppCache cache)
    : IRequestHandler<DeleteExchangeRateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteExchangeRateCommand request, CancellationToken cancellationToken)
    {
        var currentItem = await unitOfWork.RepositoryNew<CExchangeRate>().GetByIdAsync(request.Id);
        if (currentItem == null) return await Result<Guid>.FailAsync("Not found the item");
        currentItem.IsActive = false;
        await unitOfWork.RepositoryNew<CExchangeRate>().UpdateAsync(currentItem);
        await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllExchangeRateCacheKey);
        cache.Remove(Caches.GetActiveExchangeRateCacheKey);
        return await Result<Guid>.SuccessAsync(currentItem.Id, "The item deleted");
    }
}