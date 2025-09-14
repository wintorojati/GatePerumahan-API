using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Cards.UpdateCard;

public record Command(
    Guid Id,
    int PersonId,
    string CardUid,
    CardType CardType) : IRequest;
