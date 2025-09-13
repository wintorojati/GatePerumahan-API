using MediatR;
using LeafByte.Parking.CrossCutting.Pagination;

namespace LeafByte.Parking.API.Features.Persons.GetPersons;

public record Query(PaginationRequest Pagination) : IRequest<PaginatedResult<PersonDto>>;

public record PersonDto(
    int Id,
    string Name,
    char Gender,
    string Nik,
    string Address,
    string Phone,
    string LicensePlateNumber,
    DateTime CreatedAt);
