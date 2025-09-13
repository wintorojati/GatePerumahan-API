using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Devices.DeleteDevice;

public static class DeleteDeviceEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (int id, IMediator mediator, CancellationToken ct) =>
        {
            await mediator.Send(new Command(id), ct);
            return Results.NoContent();
        })
        .WithName("DeleteDevice");
    }
}
