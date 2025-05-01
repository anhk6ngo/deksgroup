namespace DTour.Application.Features.Data.Queries;

public class GetAllTourQuery : IRequest<List<TourProductionDto>?>
{
    public int KindTour { get; set; }
}

internal class GetTourQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork, IAppCache cache)
    : IRequestHandler<GetAllTourQuery, List<TourProductionDto>?>
{
    public async Task<List<TourProductionDto>?> Handle(GetAllTourQuery request,
        CancellationToken cancellationToken)
    {
        if (request.KindTour < 0)
            return await unitOfWork.RepositoryNew<TourProduction>().Entities
                .Where(w => w.IsActive == true)
                .AsNoTracking()
                .ProjectToType<TourProductionDto>()
                .ToListAsync(cancellationToken);
        Func<Task<List<TourProductionDto>>> expResult = () => unitOfWork.RepositoryNew<TourProduction>().Entities
            .Where(w => w.PublicDate != null)
            .AsNoTracking()
            .ProjectToType<TourProductionDto>()
            .ToListAsync(cancellationToken);
        var result = await cache.GetOrAddAsync(Caches.GetAllTourCacheKey, expResult,
            TimeSpan.FromMinutes(15));
        return result.Where(w => w.TourGroup == request.KindTour).ToList();
    }
}