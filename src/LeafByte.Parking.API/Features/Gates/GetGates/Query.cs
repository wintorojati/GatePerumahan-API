using MediatR;
using LeafByte.Parking.CrossCutting.Pagination;

namespace LeafByte.Parking.API.Features.Gates.GetGates;

public record Query(PaginationRequest Pagination) : IRequest<PaginatedResult<GateDto>>;

public record GateDto(
    int Id,
    string Name,
    string Code,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt);
