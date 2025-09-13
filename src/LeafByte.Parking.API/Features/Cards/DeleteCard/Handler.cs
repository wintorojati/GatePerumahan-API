using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LeafByte.Parking.API.Features.Cards.DeleteCard;

public class Handler : IRequestHandler<Command>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(Command request, CancellationToken cancellationToken)
    {
        var card = await _context.Cards
            .FirstOrDefaultAsync(c => c.Id == request.Id && c.Status == Status.Active, cancellationToken);

        if (card is null)
            return;

        card.Status = Status.Deleted;
        card.DeletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
