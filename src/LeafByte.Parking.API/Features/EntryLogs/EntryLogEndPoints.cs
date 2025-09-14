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

        // Correctly register endpoints by invoking their AddRoute methods
        CreateEntryLogEndpoint.AddRoute(group);
        GetEntryLogsEndpoint.AddRoute(group);
        GetEntryLogEndpoint.AddRoute(group);
        UpdateEntryLogEndpoint.AddRoute(group);
        DeleteEntryLogEndpoint.AddRoute(group);
    }
}
