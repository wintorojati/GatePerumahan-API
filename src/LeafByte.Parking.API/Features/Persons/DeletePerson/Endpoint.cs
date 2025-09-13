using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Persons.DeletePerson;

public class DeletePersonEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/persons/{id}", async (int id, IMediator mediator, CancellationToken ct) =>
        {
            await mediator.Send(new Command(id), ct);
            return Results.NoContent();
        })
        .WithName("DeletePerson")
        .WithTags("Person");
    }
}
