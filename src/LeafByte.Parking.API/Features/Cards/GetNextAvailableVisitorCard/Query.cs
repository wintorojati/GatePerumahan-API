using MediatR;

namespace LeafByte.Parking.API.Features.Cards.GetNextAvailableVisitorCard;

public record Query : IRequest<Response?>;

public record Response(
    Guid Id,
    string? Label,
    int? SequenceNumber,
    string CardUid);
