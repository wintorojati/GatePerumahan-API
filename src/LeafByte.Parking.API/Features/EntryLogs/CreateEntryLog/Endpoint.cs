using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.EntryLogs.CreateEntryLog;

public static class CreateEntryLogEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/entrylogs", async (Command command, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(command, ct);
            return Results.Created($"/entrylogs/{result.Id}", result);
        })
        .WithName("CreateEntryLog");
    }
}
