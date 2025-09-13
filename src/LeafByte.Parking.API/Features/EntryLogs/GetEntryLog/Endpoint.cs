using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.EntryLogs.GetEntryLog;

public static class GetEntryLogEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/entrylogs/{id}", async (int id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new Query(id), ct);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetEntryLog");
    }
}
