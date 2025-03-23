namespace DTour.Application.Features.Data.Queries;

public class GetActiveCommissionQuery : IRequest<List<CommissionDto>>
{
}

internal class GetActiveCommissionQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork, IAppCache cache)
    : IRequestHandler<GetActiveCommissionQuery, List<CommissionDto>>
{
    public async Task<List<CommissionDto>> Handle(GetActiveCommissionQuery request,
        CancellationToken cancellationToken)
    {
        var dNow = DateTime.Now;
        Func<Task<List<CommissionDto>>> expResult = () => unitOfWork.RepositoryNew<Commission>().Entities
            .Where(w => w.IsActive && w.From <= dNow && (w.To >= dNow || w.To == null))
            .ProjectToType<CommissionDto>()
            .ToListAsync(cancellationToken);
        return await cache.GetOrAddAsync(Caches.GetActiveCommissionCacheKey, expResult,
            TimeSpan.FromMinutes(15));
    }
}