namespace DTour.Application.Features.Data.Queries;

public class GetSumBookingByUserQuery : IRequest<List<SumBookingResponse>>
{
    public GetReportRequest Input { get; set; } = null!;
}

internal class GetSumBookingByUserQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<GetSumBookingByUserQuery, List<SumBookingResponse>>
{
    public async Task<List<SumBookingResponse>> Handle(GetSumBookingByUserQuery request,
        CancellationToken cancellationToken)
    {
        var oDateRange = $"{request.Input.DateRange}".ConvertRangeDate(lastday: true, isUtc: true);
        var oFilter = PredicateBuilder.True<StoreBooking>();
        oFilter = oFilter.And(w => w.CreatedOn >= oDateRange.dFrom
                                   && w.CreatedOn <= oDateRange.dTo);
        if (request.Input.UserId.NotIsNullOrEmpty())
        {
            oFilter = request.Input.Operator == 1
                ? oFilter.And(w => w.UserId == request.Input.UserId)
                : oFilter.And(w => w.CreatedBy == request.Input.UserId);
        }

        var result = await unitOfWork.RepositoryNew<StoreBooking>().Entities
            .Where(oFilter)
            .AsNoTracking()
            .Select(s => new SumBookingResponse()
            {
                TransDate = new DateTime(s.CreatedOn.Year, s.CreatedOn.Month, s.CreatedOn.Day),
                Amount = s.Amount,
                ActiveNo = s.IsActive ? 1 : 0,
                DeActiveNo = s.IsActive ? 0 : 1,
                PaymentType = s.PaymentType,
                TicketType = s.TicketType
            })
            .GroupBy(g => new
            {
                g.TicketType,
                g.TransDate,
                g.PaymentType
            }).Select(s => new SumBookingResponse()
            {
                TicketType = s.Key.TicketType,
                PaymentType = s.Key.PaymentType,
                TransDate = s.Key.TransDate,
                Amount = s.Sum(sm => sm.Amount),
                ActiveNo = s.Sum(sm => sm.ActiveNo),
                DeActiveNo = s.Sum(sm => sm.DeActiveNo)
            }).ToListAsync(cancellationToken);
        return result;
    }
}