using Carter;
using LeafByte.Parking.API.Features.Auth.Login;

namespace LeafByte.Parking.API.Features.Auth;

public static class AuthEndPoints
{
    public static void AddAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth").WithTags("Authentication");
        LoginEndpoint.AddRoute(group);
    }
}
