using MediatR;

namespace LeafByte.Parking.API.Features.Auth.Login;

public record Command(
    string Username,
    string Password) : IRequest<Response>;

public record Response(
    string Token,
    int UserId,
    string Username,
    UserRole Role);
