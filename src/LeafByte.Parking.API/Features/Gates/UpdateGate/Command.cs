using MediatR;

namespace LeafByte.Parking.API.Features.Gates.UpdateGate;

public record Command(int Id, string Name, string Code) : IRequest;
