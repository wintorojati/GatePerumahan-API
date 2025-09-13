using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Persons.CreatePerson;

public class CreatePersonEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/persons", async (Command command, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(command, ct);
            return Results.Created($"/persons/{result.Id}", result);
        })
        .WithName("CreatePerson")
        .WithTags("Person");
    }
}
