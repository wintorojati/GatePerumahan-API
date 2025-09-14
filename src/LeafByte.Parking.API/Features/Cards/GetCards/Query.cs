using MediatR;
using LeafByte.Parking.CrossCutting.Pagination;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Cards.GetCards;

public record Query(PaginationRequest Pagination) : IRequest<PaginatedResult<CardDto>>;

public record CardDto(
    Guid Id,
    int? PersonId,
    string CardUid,
    CardType CardType,
    DateTimeOffset CreatedAt,
    Status Status);
