using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Users.UpdateUser;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/users/{id:int}", async (int id, Command command, IMediator mediator, CancellationToken ct) =>
        {
            if (id != command.Id)
            {
                return Results.BadRequest("Id mismatch");
            }

            var result = await mediator.Send(command, ct);
            return Results.Ok(result);
        })
        .WithName("UpdateUser")
        .WithTags("Users");
    }
}
