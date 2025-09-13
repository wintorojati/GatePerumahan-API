using MediatR;

namespace LeafByte.Parking.API.Features.Gates.CreateGate;

public record Command(string Name, string Code) : IRequest<Response>;

public record Response(int Id);
