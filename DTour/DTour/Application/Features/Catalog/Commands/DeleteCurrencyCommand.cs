namespace DTour.Application.Features.Catalog.Commands;

public class DeleteCurrencyCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}

internal class DeleteCurrencyCommandHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<DeleteCurrencyCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currentItem = await unitOfWork.RepositoryNew<CCurrency>().GetByIdAsync(request.Id);
        if (currentItem == null) return await Result<Guid>.FailAsync("Not found the item");
        currentItem.IsActive = false;
        await unitOfWork.RepositoryNew<CCurrency>().UpdateAsync(currentItem);
        await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllCurrencyCacheKey);
        return await Result<Guid>.SuccessAsync(currentItem.Id, "The item deleted");
    }
}