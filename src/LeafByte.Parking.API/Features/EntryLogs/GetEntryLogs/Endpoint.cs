using Carter;
using MediatR;
using LeafByte.Parking.CrossCutting.Pagination;

namespace LeafByte.Parking.API.Features.EntryLogs.GetEntryLogs;

public static class GetEntryLogsEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/entrylogs", async (int? pageNumber, int? pageSize, IMediator mediator, CancellationToken ct) =>
        {
            var pagination = new PaginationRequest(
                pageNumber.HasValue ? pageNumber.Value - 1 : 0,
                pageSize ?? 10);
            var query = new Query(pagination);
            var result = await mediator.Send(query, ct);
            return Results.Ok(result);
        })
        .WithName("GetEntryLogs");
    }
}
