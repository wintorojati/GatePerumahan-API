using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Users.CreateUser;

public record Command(string Username, string Password, UserRole Role) : IRequest<Response>;

public record Response(int Id);
