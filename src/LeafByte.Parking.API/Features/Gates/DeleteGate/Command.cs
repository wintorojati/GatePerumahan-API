using MediatR;

namespace LeafByte.Parking.API.Features.Gates.DeleteGate;

public record Command(int Id) : IRequest;
