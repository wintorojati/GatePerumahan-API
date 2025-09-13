using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Users.CreateUser;

public class CreateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async (Command command, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(command, ct);
            return Results.Created($"/users/{result.Id}", result);
        })
        .WithName("CreateUser")
        .WithTags("Users");
    }
}
