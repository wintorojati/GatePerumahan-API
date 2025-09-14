using Carter;
using MediatR;

namespace LeafByte.Parking.API.Features.Cards.GetNextAvailableVisitorCard;

public static class GetNextAvailableVisitorCardEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/cards/visitor/next-available", async (IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new Query(), ct);
            return result is null ? Results.NotFound() : Results.Ok(result);
        })
        .WithName("GetNextAvailableVisitorCard");
    }
}
