namespace DTour.Application.Features.Catalog.Queries;

public class GetAllAgentQuery : IRequest<List<AgentDto>>
{
}

internal class GetAllAgentQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork, IAppCache cache)
    : IRequestHandler<GetAllAgentQuery, List<AgentDto>>
{
    public async Task<List<AgentDto>> Handle(GetAllAgentQuery request, CancellationToken cancellationToken)
    {
        Func<Task<List<AgentDto>>> expResult = () => unitOfWork.RepositoryNew<CAgent>().Entities
            .Where(w => w.IsActive == true)
            .ProjectToType<AgentDto>()
            .ToListAsync(cancellationToken);
        return await cache.GetOrAddAsync(Caches.GetAllAgentCacheKey, expResult,
            TimeSpan.FromMinutes(15));
    }
}