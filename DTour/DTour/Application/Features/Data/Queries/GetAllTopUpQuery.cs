namespace DTour.Application.Features.Data.Queries;

public class GetAllTopUpQuery : IRequest<List<TopUpDto>>
{
    public GetReportRequest Input { get; set; } = null!;
}

internal class GetAllTopUpQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<GetAllTopUpQuery, List<TopUpDto>>
{
    public async Task<List<TopUpDto>> Handle(GetAllTopUpQuery request, CancellationToken cancellationToken)
    {
        var oFilter = PredicateBuilder.True<TopUp>();
        var oDateRange = $"{request.Input.DateRange}".ConvertRangeDate(isUtc: true, lastday: true);
        oFilter = oFilter.And(w => w.IsActive);
        if (request.Input.CommandType != ActionCommandType.Approve)
        {
            oFilter = oFilter.And(w => w.RequestDate >= oDateRange.dFrom
                                       && w.RequestDate <= oDateRange.dTo);
        }

        switch (request.Input.CommandType)
        {
            case ActionCommandType.Approve:
            case ActionCommandType.View:
                if (request.Input.Status is { Count: > 0 })
                {
                    oFilter = oFilter.And(w => request.Input.Status.Contains(w.Status));
                }

                break;
            case ActionCommandType.GetData:
                oFilter = oFilter.And(w => w.UserId == request.Input.UserId);
                break;
        }

        var result = await unitOfWork.RepositoryNew<TopUp>().Entities.Where(oFilter)
            .AsNoTracking()
            .ProjectToType<TopUpDto>()
            .ToListAsync(cancellationToken);
        return result;
    }
}