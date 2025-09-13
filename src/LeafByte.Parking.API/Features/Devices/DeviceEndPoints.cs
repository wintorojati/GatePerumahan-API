using Carter;

namespace LeafByte.Parking.API.Features.Devices;

public static class DeviceEndPoints
{
    public static void AddDeviceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/devices").WithTags("Devices");

        group.MapPost("/", CreateDeviceEndpoint.AddRoute);
        group.MapGet("/", GetDevicesEndpoint.AddRoute);
        group.MapGet("/{id}", GetDeviceEndpoint.AddRoute);
        group.MapPut("/{id}", UpdateDeviceEndpoint.AddRoute);
        group.MapDelete("/{id}", DeleteDeviceEndpoint.AddRoute);
    }
}
