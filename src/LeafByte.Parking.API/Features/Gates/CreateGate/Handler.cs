using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Gates.CreateGate;

public class Handler : IRequestHandler<Command, Response>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var gate = new Gate
        {
            Name = request.Name,
            Code = request.Code,
            CreatedAt = DateTime.UtcNow,
            Status = Status.Active
        };

        _context.Gates.Add(gate);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(gate.Id);
    }
}
