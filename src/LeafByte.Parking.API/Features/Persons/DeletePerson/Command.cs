using MediatR;

namespace LeafByte.Parking.API.Features.Persons.DeletePerson;

public record Command(int Id) : IRequest;
