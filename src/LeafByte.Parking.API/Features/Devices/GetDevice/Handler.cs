using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LeafByte.Parking.API.Features.Devices.GetDevice;

public class Handler : IRequestHandler<Query, Response?>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response?> Handle(Query request, CancellationToken cancellationToken)
    {
        var device = await _context.Devices
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == request.Id && d.Status == Status.Active, cancellationToken);

        if (device is null)
            return null;

        return new Response(
            device.Id,
            device.Name,
            device.IpAddress,
            device.Type,
            device.Port,
            device.CreatedAt,
            device.UpdatedAt,
            device.Status);
    }
}
