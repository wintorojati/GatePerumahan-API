using Carter;

namespace LeafByte.Parking.API.Features.Gates;

public static class GateEndPoints
{
    public static void AddGateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/gates").WithTags("Gates");

        group.MapPost("/", CreateGateEndpoint.AddRoute);
        group.MapGet("/", GetGatesEndpoint.AddRoute);
        group.MapGet("/{id}", GetGateEndpoint.AddRoute);
        group.MapPut("/{id}", UpdateGateEndpoint.AddRoute);
        group.MapDelete("/{id}", DeleteGateEndpoint.AddRoute);
    }
}
