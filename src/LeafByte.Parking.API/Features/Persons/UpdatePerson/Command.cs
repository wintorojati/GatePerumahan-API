using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Persons.UpdatePerson;

public record Command(
    int Id,
    string Name,
    ResidentType ResidentType,
    Gender? Gender,
    IdentityType? IdentityType,
    string? IdentityNumber,
    string? Address,
    string? Phone,
    int? HouseId) : IRequest;
