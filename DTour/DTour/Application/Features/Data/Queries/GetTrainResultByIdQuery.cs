// namespace DTour.Application.Features.Data.Queries;
//
// public class GetTrainResultByIdQuery : IRequest<RailResult<List<RailCreateBookingResponse>>>
// {
//     public string? Id { get; set; }
// }
//
// internal class GetTrainResultByIdQueryHandler(IUnitOfWork<Guid, PortalContext> unitOfWork)
//     : IRequestHandler<GetTrainResultByIdQuery, RailResult<List<RailCreateBookingResponse>>>
// {
//     public async Task<RailResult<List<RailCreateBookingResponse>>> Handle(GetTrainResultByIdQuery request,
//         CancellationToken cancellationToken)
//     {
//         var result = new RailResult<List<RailCreateBookingResponse>>();
//         var id = $"{request.Id}".Replace("_", "-").ToGuid();
//         for (var i = 0; i < 3; i++)
//         {
//             var tmp = await unitOfWork.RepositoryAgg<StoreCreateTicket>().Entities
//                 .AsNoTracking()
//                 .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
//             if (tmp is { Status: -1 })
//             {
//                 result = $"{tmp.Data}".ToObject<RailResult<List<RailCreateBookingResponse>>>();
//                 if (result.Data != null || result.Data?.Count > 0)
//                 {
//                     break;
//                 }
//             }
//             await Task.Delay(1000, cancellationToken);
//         }
//         return result;
//     }
// }