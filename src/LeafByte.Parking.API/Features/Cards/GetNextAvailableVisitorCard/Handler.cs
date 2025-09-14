using MediatR;
using LeafByte.Parking.API.Data;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LeafByte.Parking.API.Features.Cards.GetNextAvailableVisitorCard;

public class Handler : IRequestHandler<Query, Response?>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response?> Handle(Query request, CancellationToken cancellationToken)
    {
        var card = await _context.Cards
            .AsNoTracking()
            .Where(c => c.CardType == CardType.Visitor && c.Status == Status.Active)
            .Where(c => !_context.VisitorInfos.Any(v => v.CardId == c.Id && v.Status == Status.Active && v.VisitEnd == null))
            .OrderBy(c => c.SequenceNumber ?? int.MaxValue)
            .ThenBy(c => c.Label)
            .Select(c => new Response(c.Id, c.Label, c.SequenceNumber, c.CardUid))
            .FirstOrDefaultAsync(cancellationToken);

        return card;
    }
}
