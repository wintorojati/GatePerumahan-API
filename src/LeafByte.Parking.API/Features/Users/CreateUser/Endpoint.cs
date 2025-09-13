using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Users.CreateUser;

public static class CreateUserEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (Command command, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(command, ct);
            return Results.Created($"/users/{result.Id}", result);
        })
        .WithName("CreateUser");
    }
}
