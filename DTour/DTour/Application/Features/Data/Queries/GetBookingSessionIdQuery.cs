namespace DTour.Application.Features.Data.Queries;

public class GetBookingSessionIdQuery : IRequest<string?>
{
    public string? Pnr { get; set; }
    public int TicketType { get; set; } = 1;
}

internal class GetBookingSessionIdQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<GetBookingSessionIdQuery, string?>
{
    public async Task<string?> Handle(GetBookingSessionIdQuery request,
        CancellationToken cancellationToken)
    {
        return await unitOfWork.RepositoryNew<StoreBooking>().Entities
            .Where(w => w.Status == 2 && w.TicketType == request.TicketType && w.Pnr == $"{request.Pnr}".ToUpper())
            .Select(s => s.BookingSessionId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}