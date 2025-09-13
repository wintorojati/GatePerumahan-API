using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Gates.CreateGate;

public static class CreateGateEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (Command command, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(command, ct);
            return Results.Created($"/gates/{result.Id}", result);
        })
        .WithName("CreateGate");
    }
}
