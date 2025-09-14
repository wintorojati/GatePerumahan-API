using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Cards.CreateCard;

public record Command(
    int? PersonId,
    string CardUid,
    CardType CardType) : IRequest<Response>;

public record Response(Guid Id);
