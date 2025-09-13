using MediatR;

namespace LeafByte.Parking.API.Features.Devices.GetDevice;

public record Query(int Id) : IRequest<Response>;

public record Response(
    int Id,
    string Name,
    string IpAddress,
    DeviceType Type,
    int Port,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    Status Status);
