using MediatR;

namespace LeafByte.Parking.API.Features.Devices.CreateDevice;

public record Command(
    string Name,
    string IpAddress,
    DeviceType Type,
    int Port) : IRequest<Response>;

public record Response(int Id);
