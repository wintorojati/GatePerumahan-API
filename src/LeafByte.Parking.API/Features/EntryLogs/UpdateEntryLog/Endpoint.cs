using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.EntryLogs.UpdateEntryLog;

public static class UpdateEntryLogEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("/entrylogs/{id}", async (int id, Command command, IMediator mediator, CancellationToken ct) =>
        {
            await mediator.Send(command with { Id = id }, ct);
            return Results.NoContent();
        })
        .WithName("UpdateEntryLog");
    }
}
