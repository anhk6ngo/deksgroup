namespace DTour.Application.Features.Data.Queries;

public class GetAllExchangeRateQuery : IRequest<List<ExchangeRateDto>>
{
}

internal class GetAllExchangeRateQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork, IAppCache cache)
    : IRequestHandler<GetAllExchangeRateQuery, List<ExchangeRateDto>>
{
    public async Task<List<ExchangeRateDto>> Handle(GetAllExchangeRateQuery request,
        CancellationToken cancellationToken)
    {
        Func<Task<List<ExchangeRateDto>>> expResult = () => unitOfWork.RepositoryNew<CExchangeRate>().Entities
            .Where(w => w.IsActive == true)
            .ProjectToType<ExchangeRateDto>()
            .ToListAsync(cancellationToken);
        return await cache.GetOrAddAsync(Caches.GetAllExchangeRateCacheKey, expResult,
            TimeSpan.FromMinutes(15));
    }
}