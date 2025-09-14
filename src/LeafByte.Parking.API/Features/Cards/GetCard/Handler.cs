using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LeafByte.Parking.API.Features.Cards.GetCard;

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
            .FirstOrDefaultAsync(c => c.Id == request.Id && c.Status == Status.Active, cancellationToken);

        if (card is null)
            return null;

        return new Response(
            card.Id,
            card.PersonId,
            card.CardUid,
            card.CardType,
            card.CreatedAt,
            card.UpdatedAt,
            card.Status);
    }
}
