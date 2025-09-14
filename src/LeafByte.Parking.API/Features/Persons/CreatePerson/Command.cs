using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Persons.CreatePerson;

public record Command(
    string Name,
    ResidentType ResidentType,
    Gender? Gender,
    IdentityType? IdentityType,
    string? IdentityNumber,
    string? Address,
    string? Phone,
    int? HouseId) : IRequest<Response>;

public record Response(int Id);
