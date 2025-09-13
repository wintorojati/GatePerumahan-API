using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Cards.CreateCard;

public static class CreateCardEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/cards", async (Command command, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(command, ct);
            return Results.Created($"/cards/{result.Id}", result);
        })
        .WithName("CreateCard");
    }
}
