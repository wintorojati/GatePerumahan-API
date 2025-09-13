using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Users.UpdateUser;

public record Command(int Id, string Username, UserRole Role, Status Status) : IRequest<Response>;

public record Response(bool Success);
