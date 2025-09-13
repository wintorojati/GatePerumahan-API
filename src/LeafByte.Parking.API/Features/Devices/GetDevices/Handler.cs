using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;
using LeafByte.Parking.CrossCutting.Pagination;

namespace LeafByte.Parking.API.Features.Devices.GetDevices;

public class Handler : IRequestHandler<Query, PaginatedResult<DeviceDto>>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<DeviceDto>> Handle(Query request, CancellationToken cancellationToken)
    {
        var query = _context.Devices
            .Where(d => d.Status == Status.Active)
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var paginatedQuery = query
            .Skip(request.Pagination.PageIndex * request.Pagination.PageSize)
            .Take(request.Pagination.PageSize);

        var devices = await paginatedQuery
            .Select(d => new DeviceDto(
                d.Id,
                d.Name,
                d.IpAddress,
                d.Type,
                d.Port,
                d.CreatedAt))
            .ToListAsync(cancellationToken);

        return new PaginatedResult<DeviceDto>(
            request.Pagination.PageIndex,
            request.Pagination.PageSize,
            totalCount,
            devices);
    }
}
