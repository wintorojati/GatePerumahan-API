using MediatR;

namespace LeafByte.Parking.API.Features.Persons.UpdatePerson;

public record Command(
    int Id,
    string Name,
    char Gender,
    string Nik,
    string Address,
    string Phone,
    string LicensePlateNumber) : IRequest;
