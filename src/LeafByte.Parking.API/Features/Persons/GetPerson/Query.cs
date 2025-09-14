using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Persons.GetPerson;

public record Query(int Id) : IRequest<Response>;

public record Response(
    int Id,
    string Name,
    ResidentType ResidentType,
    Gender? Gender,
    IdentityType? IdentityType,
    string? IdentityNumber,
    string? Address,
    string? Phone,
    int? HouseId,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt,
    Status Status);
