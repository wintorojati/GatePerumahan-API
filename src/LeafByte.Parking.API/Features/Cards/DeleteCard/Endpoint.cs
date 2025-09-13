using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Cards.DeleteCard;

public static class DeleteCardEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("/cards/{id}", async (Guid id, IMediator mediator, CancellationToken ct) =>
        {
            await mediator.Send(new Command(id), ct);
            return Results.NoContent();
        })
        .WithName("DeleteCard");
    }
}
