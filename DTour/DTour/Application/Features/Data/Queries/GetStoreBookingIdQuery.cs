namespace DTour.Application.Features.Data.Queries;

public class GetStoreBookingIdQuery : IRequest<StoreBookingDto?>
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
}

internal class GetStoreBookingIdQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<GetStoreBookingIdQuery, StoreBookingDto?>
{
    public async Task<StoreBookingDto?> Handle(GetStoreBookingIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = new StoreBookingDto();
        for (var i = 0; i < 3; i++)
        {
            result =await unitOfWork.RepositoryNew<StoreBooking>().Entities
                .Where(w=>w.Id == request.Id && w.UserId == request.UserId)
                .ProjectToType<StoreBookingDto>()
                .FirstOrDefaultAsync(cancellationToken);
            if (result != null && result.TransactionId.NotIsNullOrEmpty())
            {
                break;
            }
            await Task.Delay(1000, cancellationToken);
        }
        return result;
    }
}