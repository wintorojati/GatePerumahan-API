using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Devices.CreateDevice;

public class Handler : IRequestHandler<Command, Response>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var device = new Device
        {
            Name = request.Name,
            IpAddress = request.IpAddress,
            Type = request.Type,
            Port = request.Port,
            CreatedAt = DateTime.UtcNow,
            Status = Status.Active
        };

        _context.Devices.Add(device);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(device.Id);
    }
}
