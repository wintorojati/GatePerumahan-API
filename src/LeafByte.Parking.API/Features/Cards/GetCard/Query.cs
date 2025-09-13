using MediatR;

namespace LeafByte.Parking.API.Features.Cards.GetCard;

public record Query(Guid Id) : IRequest<Response?>;

public record Response(
    Guid Id,
    int PersonId,
    string CardUid,
    AccessType AccessType,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    Status Status);
