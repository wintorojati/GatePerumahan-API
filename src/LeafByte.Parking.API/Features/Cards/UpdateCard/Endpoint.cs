using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Cards.UpdateCard;

public static class UpdateCardEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("/cards/{id}", async (Guid id, Command command, IMediator mediator, CancellationToken ct) =>
        {
            await mediator.Send(command with { Id = id }, ct);
            return Results.NoContent();
        })
        .WithName("UpdateCard");
    }
}
