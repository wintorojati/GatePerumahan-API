using MediatR;
using LeafByte.Parking.CrossCutting.Pagination;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Persons.GetPersons;

public record Query(PaginationRequest Pagination) : IRequest<PaginatedResult<PersonDto>>;

public record PersonDto(
    int Id,
    string Name,
    ResidentType ResidentType,
    Gender? Gender,
    IdentityType? IdentityType,
    string? IdentityNumber,
    string? Address,
    string? Phone,
    int? HouseId,
    DateTimeOffset CreatedAt);
