using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Cards.GetCard;

public static class GetCardEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/cards/{id}", async (Guid id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new Query(id), ct);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetCard");
    }
}
