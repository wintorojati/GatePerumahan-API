using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LeafByte.Parking.API.Features.Gates.GetGate;

public class Handler : IRequestHandler<Query, Response?>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response?> Handle(Query request, CancellationToken cancellationToken)
    {
        var gate = await _context.Gates
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == request.Id && g.Status == Status.Active, cancellationToken);

        if (gate is null)
            return null;

        return new Response(
            gate.Id,
            gate.Name,
            gate.Code,
            gate.CreatedAt,
            gate.UpdatedAt,
            gate.Status);
    }
}
