using Carter;
using LeafByte.Parking.API.Features.Gates.CreateGate;
using LeafByte.Parking.API.Features.Gates.DeleteGate;
using LeafByte.Parking.API.Features.Gates.GetGate;
using LeafByte.Parking.API.Features.Gates.GetGates;
using LeafByte.Parking.API.Features.Gates.UpdateGate;
using Microsoft.AspNetCore.Authorization;

namespace LeafByte.Parking.API.Features.Gates;

public static class GateEndPoints
{
    public static void AddGateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/gates").WithTags("Gates").RequireAuthorization();

        group.MapPost("/", CreateGateEndpoint.AddRoute);
        group.MapGet("/", GetGatesEndpoint.AddRoute);
        group.MapGet("/{id}", GetGateEndpoint.AddRoute);
        group.MapPut("/{id}", UpdateGateEndpoint.AddRoute);
        group.MapDelete("/{id}", DeleteGateEndpoint.AddRoute);
    }
}
