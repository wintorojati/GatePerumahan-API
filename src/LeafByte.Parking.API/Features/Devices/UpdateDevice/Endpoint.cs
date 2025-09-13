using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Devices.UpdateDevice;

public static class UpdateDeviceEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", async (int id, Command command, IMediator mediator, CancellationToken ct) =>
        {
            await mediator.Send(command with { Id = id }, ct);
            return Results.NoContent();
        })
        .WithName("UpdateDevice");
    }
}
