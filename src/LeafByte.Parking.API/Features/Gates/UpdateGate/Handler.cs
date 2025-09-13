using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LeafByte.Parking.API.Features.Gates.UpdateGate;

public class Handler : IRequestHandler<Command>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(Command request, CancellationToken cancellationToken)
    {
        var gate = await _context.Gates
            .FirstOrDefaultAsync(g => g.Id == request.Id && g.Status == Status.Active, cancellationToken);

        if (gate is null)
            return;

        gate.Name = request.Name;
        gate.Code = request.Code;
        gate.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
