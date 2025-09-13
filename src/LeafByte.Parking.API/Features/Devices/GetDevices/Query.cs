using MediatR;
using LeafByte.Parking.CrossCutting.Pagination;

namespace LeafByte.Parking.API.Features.Devices.GetDevices;

public record Query(PaginationRequest Pagination) : IRequest<PaginatedResult<DeviceDto>>;

public record DeviceDto(
    int Id,
    string Name,
    string IpAddress,
    DeviceType Type,
    int Port,
    DateTime CreatedAt);
