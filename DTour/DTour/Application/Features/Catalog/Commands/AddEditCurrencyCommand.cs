namespace DTour.Application.Features.Catalog.Commands;

public class AddEditCurrencyCommand : IRequest<Result<Guid>>
{
    public AddEditDataRequest<CurrencyDto> Request { get; set; } = default!;
}

internal class AddEditCurrencyCommandHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<AddEditCurrencyCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddEditCurrencyCommand command, CancellationToken cancellationToken)
    {
        switch (command.Request.Action)
        {
            case ActionCommandType.Add:
                var oNewItem = command.Request.Data.Adapt<CCurrency>();
                await unitOfWork.RepositoryNew<CCurrency>().AddAsync(oNewItem);
                await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllCurrencyCacheKey);
                return await Result<Guid>.SuccessAsync(oNewItem.Id, "The item added");
            default:
                var currentItem = await unitOfWork.RepositoryNew<CCurrency>().GetByIdAsync(command.Request.Data!.Id);
                if (currentItem != null)
                {
                    command.Request.Data.Adapt(currentItem);
                    await unitOfWork.RepositoryNew<CCurrency>().UpdateAsync(currentItem);
                    await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllCurrencyCacheKey);
                    return await Result<Guid>.SuccessAsync(currentItem.Id, "The item updated");
                }

                break;
        }

        return await Result<Guid>.FailAsync("Not found the item");
    }
}