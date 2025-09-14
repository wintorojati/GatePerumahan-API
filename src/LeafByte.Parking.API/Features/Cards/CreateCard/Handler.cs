using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Cards.CreateCard;

public class Handler : IRequestHandler<Command, Response>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var card = new Card
        {
            PersonId = request.PersonId,
            CardUid = request.CardUid,
            CardType = request.CardType,
            CreatedAt = DateTimeOffset.UtcNow,
            Status = Status.Active
        };

        _context.Cards.Add(card);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(card.Id);
    }
}
