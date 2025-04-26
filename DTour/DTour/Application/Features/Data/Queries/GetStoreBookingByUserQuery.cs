namespace DTour.Application.Features.Data.Queries;

public class GetStoreBookingByUserQuery : IRequest<List<StoreBookingDto>>
{
    public GetReportRequest Input { get; set; } = null!;
}

internal class GetStoreBookingByUserQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<GetStoreBookingByUserQuery, List<StoreBookingDto>>
{
    public async Task<List<StoreBookingDto>> Handle(GetStoreBookingByUserQuery request,
        CancellationToken cancellationToken)
    {
        var oDateRange = $"{request.Input.DateRange}".ConvertRangeDate(lastday: true, isUtc: true);
        var oFilter = PredicateBuilder.True<StoreBooking>();
        oFilter = oFilter.And(w => w.IsActive && w.Status > 0 && w.TicketType >= 0
                                   && w.CreatedOn >= oDateRange.dFrom && w.CreatedOn <= oDateRange.dTo);
        if (request.Input.UserId.NotIsNullOrEmpty())
        {
            oFilter = request.Input.Operator == 1 ? oFilter.And(w => w.UserId == request.Input.UserId) : oFilter.And(w => w.CreatedBy == request.Input.UserId);
        }

        var result = await unitOfWork.RepositoryNew<StoreBooking>().Entities
            .Where(oFilter)
            .AsNoTracking()
            .ProjectToType<StoreBookingDto>()
            .ToListAsync(cancellationToken);
        return result;
    }
}