using MediatR;

namespace LeafByte.Parking.API.Features.Persons.GetPerson;

public record Query(int Id) : IRequest<Response>;

public record Response(
    int Id,
    string Name,
    char Gender,
    string Nik,
    string Address,
    string Phone,
    string LicensePlateNumber,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    Status Status);
