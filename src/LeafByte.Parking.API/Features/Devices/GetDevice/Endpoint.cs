using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Devices.GetDevice;

public static class GetDeviceEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (int id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new Query(id), ct);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetDevice");
    }
}
