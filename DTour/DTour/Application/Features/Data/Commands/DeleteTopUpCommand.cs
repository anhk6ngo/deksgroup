namespace DTour.Application.Features.Data.Commands;

public class DeleteTopUpCommand : IRequest<ResultClient<Guid>>
{
    public Guid Id { get; set; }
}

internal class DeleteTopUpCommandHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<DeleteTopUpCommand, ResultClient<Guid>>
{
    public async Task<ResultClient<Guid>> Handle(DeleteTopUpCommand request, CancellationToken cancellationToken)
    {
        var currentItem = await unitOfWork.RepositoryNew<TopUp>().GetByIdAsync(request.Id);
        if (currentItem == null) return await ResultClient<Guid>.FailAsync("Not found the item");
        currentItem.IsActive = false;
        await unitOfWork.RepositoryNew<TopUp>().UpdateAsync(currentItem);
        await unitOfWork.Commit(cancellationToken);
        return await ResultClient<Guid>.SuccessAsync(currentItem.Id, "The item deleted");
    }
}