namespace DTour.Application.Features.Data.Queries;

public class GetAllPostQuery : IRequest<List<PostDto>?>
{
    public int KindPost { get; set; }
}

internal class GetPostQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork, IAppCache cache)
    : IRequestHandler<GetAllPostQuery, List<PostDto>?>
{
    public async Task<List<PostDto>?> Handle(GetAllPostQuery request,
        CancellationToken cancellationToken)
    {
        if (request.KindPost < 0)
            return await unitOfWork.RepositoryNew<Post>().Entities
                .Where(w => w.IsActive == true)
                .AsNoTracking()
                .ProjectToType<PostDto>()
                .ToListAsync(cancellationToken);
        Func<Task<List<PostDto>>> expResult = () => unitOfWork.RepositoryNew<Post>().Entities
            .Where(w => w.PublicDate != null)
            .AsNoTracking()
            .ProjectToType<PostDto>()
            .ToListAsync(cancellationToken);
        var result = await cache.GetOrAddAsync(Caches.GetAllPostCacheKey, expResult,
            TimeSpan.FromMinutes(15));
        return result.Where(w => w.Kind == request.KindPost).ToList();
    }
}