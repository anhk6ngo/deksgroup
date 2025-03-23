namespace DTour.Application.Features.Data.Commands;

public class AddEditExchangeRateCommand : IRequest<Result<Guid>>
{
    public AddEditDataRequest<ExchangeRateDto> Request { get; set; } = default!;
}

internal class AddEditExchangeRateCommandHandler(IUnitOfWork<Guid, PortalContext> unitOfWork, IAppCache cache)
    : IRequestHandler<AddEditExchangeRateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddEditExchangeRateCommand command, CancellationToken cancellationToken)
    {
        switch (command.Request.Action)
        {
            case ActionCommandType.Add:
                var oNewItem = command.Request.Data.Adapt<CExchangeRate>();
                await unitOfWork.RepositoryNew<CExchangeRate>().AddAsync(oNewItem);
                await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllExchangeRateCacheKey);
                cache.Remove(Caches.GetActiveExchangeRateCacheKey);
                return await Result<Guid>.SuccessAsync(oNewItem.Id, "The item added");
            default:
                var currentItem = await unitOfWork.RepositoryNew<CExchangeRate>().GetByIdAsync(command.Request.Data!.Id);
                if (currentItem != null)
                {
                    command.Request.Data.Adapt(currentItem);
                    await unitOfWork.RepositoryNew<CExchangeRate>().UpdateAsync(currentItem);
                    await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllExchangeRateCacheKey);
                    cache.Remove(Caches.GetActiveExchangeRateCacheKey);
                    return await Result<Guid>.SuccessAsync(currentItem.Id, "The item updated");
                }

                break;
        }

        return await Result<Guid>.FailAsync("Not found the item");
    }
}