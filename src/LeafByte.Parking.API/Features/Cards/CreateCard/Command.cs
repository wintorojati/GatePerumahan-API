using MediatR;

namespace LeafByte.Parking.API.Features.Cards.CreateCard;

public record Command(
    int PersonId,
    string CardUid,
    AccessType AccessType) : IRequest<Response>;

public record Response(Guid Id);
