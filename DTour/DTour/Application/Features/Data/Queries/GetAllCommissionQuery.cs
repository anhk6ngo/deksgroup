namespace DTour.Application.Features.Data.Queries;

public class GetAllCommissionQuery : IRequest<List<CommissionDto>>
{
}

internal class GetAllCommissionQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork, IAppCache cache)
    : IRequestHandler<GetAllCommissionQuery, List<CommissionDto>>
{
    public async Task<List<CommissionDto>> Handle(GetAllCommissionQuery request,
        CancellationToken cancellationToken)
    {
        Func<Task<List<CommissionDto>>> expResult = () => unitOfWork.RepositoryNew<Commission>().Entities
            .Where(w => w.IsActive == true)
            .ProjectToType<CommissionDto>()
            .ToListAsync(cancellationToken);
        return await cache.GetOrAddAsync(Caches.GetAllCommissionCacheKey, expResult,
            TimeSpan.FromMinutes(15));
    }
}