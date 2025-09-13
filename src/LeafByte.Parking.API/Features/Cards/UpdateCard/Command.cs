using MediatR;

namespace LeafByte.Parking.API.Features.Cards.UpdateCard;

public record Command(
    Guid Id,
    int PersonId,
    string CardUid,
    AccessType AccessType) : IRequest;
