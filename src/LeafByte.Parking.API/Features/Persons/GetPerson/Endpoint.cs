using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Persons.GetPerson;

public static class GetPersonEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (int id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new Query(id), ct);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetPerson");
    }
}
