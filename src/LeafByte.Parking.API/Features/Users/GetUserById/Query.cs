using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Users.GetUserById;

public record Query(int Id) : IRequest<Response>;

public record Response(UserDto User);
