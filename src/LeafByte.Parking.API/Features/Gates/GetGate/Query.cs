using MediatR;

namespace LeafByte.Parking.API.Features.Gates.GetGate;

public record Query(int Id) : IRequest<Response>;

public record Response(
    int Id,
    string Name,
    string Code,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    Status Status);
