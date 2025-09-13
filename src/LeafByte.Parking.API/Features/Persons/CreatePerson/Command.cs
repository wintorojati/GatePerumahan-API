using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Persons.CreatePerson;

public record Command(
    string Name,
    char Gender,
    string Nik,
    string Address,
    string Phone,
    string LicensePlateNumber) : IRequest<Response>;

public record Response(int Id);
