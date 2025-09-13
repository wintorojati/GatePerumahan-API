using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Users.GetUserById;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id:int}", async (int id, IMediator mediator, CancellationToken ct) =>
        {
            var query = new Query(id);
            var result = await mediator.Send(query, ct);
            return Results.Ok(result.User);
        })
        .WithName("GetUserById")
        .WithTags("Users");
    }
}
