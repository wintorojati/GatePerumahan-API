using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Devices.GetDevice;

public record Query(int Id) : IRequest<Response>;

public record Response(
    int Id,
    string Name,
    string IpAddress,
    DeviceType Type,
    int Port,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt,
    Status Status);
