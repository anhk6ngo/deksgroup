namespace DTour.Application.Features.Data.Commands;

public class AddEditTourCommand : IRequest<Result<Guid>>
{
    public AddEditDataRequest<TourProductionDto> Request { get; set; } = default!;
}

internal class AddEditTourCommandHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<AddEditTourCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddEditTourCommand command, CancellationToken cancellationToken)
    {
        switch (command.Request.Action)
        {
            case ActionCommandType.Add:
                var oNewItem = command.Request.Data.Adapt<TourProduction>();
                await unitOfWork.RepositoryNew<TourProduction>().AddAsync(oNewItem);
                await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllTourCacheKey);
                return await Result<Guid>.SuccessAsync(oNewItem.Id, "The item added");
            default:
                var currentItem = await unitOfWork.RepositoryNew<TourProduction>().GetByIdAsync(command.Request.Data!.Id);
                if (currentItem != null)
                {
                    command.Request.Data.Adapt(currentItem);
                    await unitOfWork.RepositoryNew<TourProduction>().UpdateAsync(currentItem);
                    await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllTourCacheKey);
                    return await Result<Guid>.SuccessAsync(currentItem.Id, "The item updated");
                }
                break;
        }
        return await Result<Guid>.FailAsync("Not found the item");
    }
}