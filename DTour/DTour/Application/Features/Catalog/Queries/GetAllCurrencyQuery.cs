namespace DTour.Application.Features.Catalog.Queries;

public class GetAllCurrencyQuery : IRequest<List<CurrencyDto>>
{
}

internal class GetAllCurrencyQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork, IAppCache cache)
    : IRequestHandler<GetAllCurrencyQuery, List<CurrencyDto>>
{
    public async Task<List<CurrencyDto>> Handle(GetAllCurrencyQuery request, CancellationToken cancellationToken)
    {
        Func<Task<List<CurrencyDto>>> expResult = () => unitOfWork.RepositoryNew<CCurrency>().Entities
            .Where(w => w.IsActive == true)
            .ProjectToType<CurrencyDto>()
            .ToListAsync(cancellationToken);
        return await cache.GetOrAddAsync(Caches.GetAllCurrencyCacheKey, expResult,
            TimeSpan.FromMinutes(15));
    }
}