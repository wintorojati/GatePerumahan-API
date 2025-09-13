using MediatR;

namespace LeafByte.Parking.API.Features.Devices.UpdateDevice;

public record Command(
    int Id,
    string Name,
    string IpAddress,
    DeviceType Type,
    int Port) : IRequest;
