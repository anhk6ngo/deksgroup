namespace DTour.Application.Features.Data.Commands;

public class AddEditBookingCommand : IRequest<ResultClient<Guid>>
{
    public AddEditDataRequest<StoreBookingDto> Request { get; set; } = default!;
}

internal class AddEditBookingCommandHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<AddEditBookingCommand, ResultClient<Guid>>
{
    public async Task<ResultClient<Guid>> Handle(AddEditBookingCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            switch (command.Request.Action)
            {
                case ActionCommandType.Add:
                    var oNewItem = command.Request.Data.Adapt<StoreBooking>();
                    await unitOfWork.RepositoryNew<StoreBooking>().AddAsync(oNewItem);
                    await unitOfWork.Commit(cancellationToken);
                    return await ResultClient<Guid>.SuccessAsync(oNewItem.Id, "The item added");
                default:
                    break;
            }
            return await ResultClient<Guid>.FailAsync("Not found the item");
        }
        catch (Exception e)
        {
            return await ResultClient<Guid>.FailAsync($"{e.Message}");
        }
    }
}