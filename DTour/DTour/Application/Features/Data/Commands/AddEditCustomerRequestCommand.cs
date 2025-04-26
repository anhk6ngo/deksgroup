namespace DTour.Application.Features.Data.Commands;

public class AddEditCustomerRequestCommand : IRequest<Result<Guid>>
{
    public AddEditDataRequest<CustomerRequestDto> Request { get; set; } = default!;
}

internal class AddEditCustomerRequestCommandHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<AddEditCustomerRequestCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddEditCustomerRequestCommand command, CancellationToken cancellationToken)
    {
        switch (command.Request.Action)
        {
            case ActionCommandType.Add:
                var oNewItem = command.Request.Data.Adapt<CustomerRequest>();
                await unitOfWork.RepositoryNew<CustomerRequest>().AddAsync(oNewItem);
                await unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(oNewItem.Id, "The item added");
            default:
                var currentItem = await unitOfWork.RepositoryNew<CustomerRequest>().GetByIdAsync(command.Request.Data!.Id);
                if (currentItem != null)
                {
                    command.Request.Data.Adapt(currentItem);
                    await unitOfWork.RepositoryNew<CustomerRequest>().UpdateAsync(currentItem);
                    await unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(currentItem.Id, "The item updated");
                }

                break;
        }

        return await Result<Guid>.FailAsync("Not found the item");
    }
}