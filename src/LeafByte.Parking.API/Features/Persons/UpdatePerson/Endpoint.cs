using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Persons.UpdatePerson;

public static class UpdatePersonEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
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
