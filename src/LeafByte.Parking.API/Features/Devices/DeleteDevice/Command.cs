using MediatR;

namespace LeafByte.Parking.API.Features.Devices.DeleteDevice;

public record Command(int Id) : IRequest;
