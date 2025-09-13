using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Auth.Login;

public static class LoginEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login", async (Command command, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(command, ct);
            return Results.Ok(result);
        })
        .WithName("Login");
    }
}
