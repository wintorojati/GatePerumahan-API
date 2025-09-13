using MediatR;

namespace LeafByte.Parking.API.Features.Users.DeleteUser;

public record Command(int Id) : IRequest<Response>;

public record Response(bool Success);
