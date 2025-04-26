namespace DTour.Application.Features.Data.Commands;

public class AddEditPostCommand : IRequest<Result<Guid>>
{
    public AddEditDataRequest<PostDto> Request { get; set; } = default!;
}

internal class AddEditPostCommandHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<AddEditPostCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddEditPostCommand command, CancellationToken cancellationToken)
    {
        switch (command.Request.Action)
        {
            case ActionCommandType.Add:
                var oNewItem = command.Request.Data.Adapt<Post>();
                await unitOfWork.RepositoryNew<Post>().AddAsync(oNewItem);
                await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllPostCacheKey);
                return await Result<Guid>.SuccessAsync(oNewItem.Id, "The item added");
            default:
                var currentItem = await unitOfWork.RepositoryNew<Post>().GetByIdAsync(command.Request.Data!.Id);
                if (currentItem != null)
                {
                    command.Request.Data.Adapt(currentItem);
                    await unitOfWork.RepositoryNew<Post>().UpdateAsync(currentItem);
                    await unitOfWork.CommitAndRemoveCache(cancellationToken, Caches.GetAllPostCacheKey);
                    return await Result<Guid>.SuccessAsync(currentItem.Id, "The item updated");
                }
                break;
        }
        return await Result<Guid>.FailAsync("Not found the item");
    }
}