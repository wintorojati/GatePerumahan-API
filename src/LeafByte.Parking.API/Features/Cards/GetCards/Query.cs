using MediatR;
using LeafByte.Parking.CrossCutting.Pagination;

namespace LeafByte.Parking.API.Features.Cards.GetCards;

public record Query(PaginationRequest Pagination) : IRequest<PaginatedResult<CardDto>>;

public record CardDto(
    Guid Id,
    int PersonId,
    string CardUid,
    AccessType AccessType,
    DateTime CreatedAt,
    Status Status);
