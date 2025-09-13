using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Devices.CreateDevice;

public static class CreateDeviceEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (Command command, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(command, ct);
            return Results.Created($"/devices/{result.Id}", result);
        })
        .WithName("CreateDevice");
    }
}
