using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Persons.UpdatePerson;

public class UpdatePersonEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/persons/{id}", async (int id, Command command, IMediator mediator, CancellationToken ct) =>
        {
            await mediator.Send(command with { Id = id }, ct);
            return Results.NoContent();
        })
        .WithName("UpdatePerson")
        .WithTags("Person");
    }
}
