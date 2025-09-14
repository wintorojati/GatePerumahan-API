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

        CreateGateEndpoint.AddRoute(group);
        GetGatesEndpoint.AddRoute(group);
        GetGateEndpoint.AddRoute(group);
        UpdateGateEndpoint.AddRoute(group);
        DeleteGateEndpoint.AddRoute(group);
    }
}
