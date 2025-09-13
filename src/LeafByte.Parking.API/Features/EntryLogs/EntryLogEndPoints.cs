using Carter;
using Microsoft.AspNetCore.Authorization;

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
