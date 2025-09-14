using Carter;
using LeafByte.Parking.API.Features.Devices.CreateDevice;
using LeafByte.Parking.API.Features.Devices.DeleteDevice;
using LeafByte.Parking.API.Features.Devices.GetDevice;
using LeafByte.Parking.API.Features.Devices.GetDevices;
using LeafByte.Parking.API.Features.Devices.UpdateDevice;
using Microsoft.AspNetCore.Authorization;

namespace LeafByte.Parking.API.Features.Devices;

public static class DeviceEndPoints
{
    public static void AddDeviceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/devices").WithTags("Devices").RequireAuthorization();

        // Call AddRoute to register endpoints on the group
        CreateDeviceEndpoint.AddRoute(group);
        GetDevicesEndpoint.AddRoute(group);
        GetDeviceEndpoint.AddRoute(group);
        UpdateDeviceEndpoint.AddRoute(group);
        DeleteDeviceEndpoint.AddRoute(group);
    }
}
