namespace DTour.Application.Features.Data.Queries;

public class GetCashFlowByUserQuery : IRequest<List<CashFlowResponse>>
{
    public GetReportRequest Input { get; set; } = null!;
}

internal class GetCashFlowByUserQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<GetCashFlowByUserQuery, List<CashFlowResponse>>
{
    public async Task<List<CashFlowResponse>> Handle(GetCashFlowByUserQuery request,
        CancellationToken cancellationToken)
    {
        var oDateRange = $"{request.Input.DateRange}".ConvertRangeDate(lastday: true, isUtc: true);
        var oFilter = PredicateBuilder.True<StoreBooking>();
        oFilter = oFilter.And(w => w.CreatedOn >= oDateRange.dFrom
                                   && w.CreatedOn <= oDateRange.dTo);
        if (request.Input.UserId.NotIsNullOrEmpty())
        {
            oFilter = oFilter.And(w => w.UserId == request.Input.UserId);
        }

        var oFilterIn = PredicateBuilder.True<TopUp>();
        oFilterIn = oFilterIn.And(w => w.ApproveDate >= oDateRange.dFrom
                                       && w.ApproveDate <= oDateRange.dTo);
        if (request.Input.UserId.NotIsNullOrEmpty())
        {
            oFilterIn = oFilterIn.And(w => w.UserId == request.Input.UserId);
        }

        var resultIn = await unitOfWork.RepositoryNew<TopUp>().Entities
            .Where(oFilterIn)
            .AsNoTracking()
            .Select(s => new CashFlowResponse()
            {
                TranDate = s.ApproveDate,
                In = s.ApproveAmount,
                UserId = s.UserId,
            }).ToListAsync(cancellationToken);
        var resultBalance = await unitOfWork.RepositoryAgg<UserBalance>().Entities
            .AsNoTracking()
            .Select(s => new CashFlowResponse()
            {
                TranDate = oDateRange.dTo.AddMinutes(1),
                Balance = s.Amount,
                UserId = s.Id.ToString(),
            }).ToListAsync(cancellationToken);
        var result = await unitOfWork.RepositoryNew<StoreBooking>().Entities
            .Where(oFilter)
            .AsNoTracking()
            .Select(s => new CashFlowResponse()
            {
                TranDate = s.CreatedOn,
                Out = s.Amount,
                UserId = s.UserId,
                PaymentType = s.PaymentType,
                Pnr = s.Pnr
            }).ToListAsync(cancellationToken);
        resultIn.AddRange(resultBalance);
        result.AddRange(resultIn);
        return result;
    }
}