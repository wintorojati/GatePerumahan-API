using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Gates.UpdateGate;

public static class UpdateGateEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("/gates/{id}", async (int id, Command command, IMediator mediator, CancellationToken ct) =>
        {
            await mediator.Send(command with { Id = id }, ct);
            return Results.NoContent();
        })
        .WithName("UpdateGate")
        .WithTags("Gate");
    }
}
