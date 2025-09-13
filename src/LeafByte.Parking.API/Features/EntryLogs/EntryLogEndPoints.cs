using Carter;
using Microsoft.AspNetCore.Authorization;
using LeafByte.Parking.API.Features.EntryLogs.CreateEntryLog;
using LeafByte.Parking.API.Features.EntryLogs.GetEntryLogs;
using LeafByte.Parking.API.Features.EntryLogs.GetEntryLog;
using LeafByte.Parking.API.Features.EntryLogs.UpdateEntryLog;
using LeafByte.Parking.API.Features.EntryLogs.DeleteEntryLog;

namespace LeafByte.Parking.API.Features.EntryLogs;

public static class EntryLogEndPoints
{
    public static void AddEntryLogEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/entrylogs").WithTags("EntryLogs").RequireAuthorization();

        group.MapPost("/", CreateEntryLogEndpoint.AddRoute);
        group.MapGet("/", GetEntryLogsEndpoint.AddRoute);
        group.MapGet("/{id}", GetEntryLogEndpoint.AddRoute);
        group.MapPut("/{id}", UpdateEntryLogEndpoint.AddRoute);
        group.MapDelete("/{id}", DeleteEntryLogEndpoint.AddRoute);
    }
}
