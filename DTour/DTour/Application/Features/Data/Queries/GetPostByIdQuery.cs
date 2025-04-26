namespace DTour.Application.Features.Data.Queries;

public class GetPostByIdQuery : IRequest<PostDto?>
{
    public Guid Id { get; set; }
}

internal class GetPostByIdQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<GetPostByIdQuery, PostDto?>
{
    public async Task<PostDto?> Handle(GetPostByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await unitOfWork.RepositoryNew<Post>().Entities
            .ProjectToType<PostDto>()
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);
    }
}