using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Cards.GetCard;

public record Query(Guid Id) : IRequest<Response?>;

public record Response(
    Guid Id,
    int? PersonId,
    string CardUid,
    CardType CardType,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt,
    Status Status);
