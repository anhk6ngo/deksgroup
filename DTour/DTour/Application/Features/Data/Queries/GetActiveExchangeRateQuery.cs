namespace DTour.Application.Features.Data.Queries;

public class GetActiveExchangeRateQuery : IRequest<List<ExchangeRateDto>>
{
}

internal class GetActiveExchangeRateQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork, IAppCache cache)
    : IRequestHandler<GetActiveExchangeRateQuery, List<ExchangeRateDto>>
{
    public async Task<List<ExchangeRateDto>> Handle(GetActiveExchangeRateQuery request,
        CancellationToken cancellationToken)
    {
        var dNow = DateTime.Now;
        Func<Task<List<ExchangeRateDto>>> expResult = () => unitOfWork.RepositoryNew<CExchangeRate>().Entities
            .Where(w => w.IsActive && w.From <= dNow && (w.To >= dNow || w.To == null))
            .ProjectToType<ExchangeRateDto>()
            .ToListAsync(cancellationToken);
        return await cache.GetOrAddAsync(Caches.GetActiveExchangeRateCacheKey, expResult,
            TimeSpan.FromMinutes(15));
    }
}