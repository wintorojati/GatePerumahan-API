using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.EntryLogs.DeleteEntryLog;

public static class DeleteEntryLogEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("/entrylogs/{id}", async (int id, IMediator mediator, CancellationToken ct) =>
        {
            await mediator.Send(new Command(id), ct);
            return Results.NoContent();
        })
        .WithName("DeleteEntryLog");
    }
}
