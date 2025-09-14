using Carter;
using Microsoft.AspNetCore.Authorization;
using LeafByte.Parking.API.Features.Cards.CreateCard;
using LeafByte.Parking.API.Features.Cards.GetCards;
using LeafByte.Parking.API.Features.Cards.GetCard;
using LeafByte.Parking.API.Features.Cards.UpdateCard;
using LeafByte.Parking.API.Features.Cards.DeleteCard;
using LeafByte.Parking.API.Features.Cards.GetNextAvailableVisitorCard;

namespace LeafByte.Parking.API.Features.Cards;

public static class CardEndPoints
{
    public static void AddCardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/cards").WithTags("Cards").RequireAuthorization();

        // Properly register endpoints by calling their AddRoute methods
        CreateCardEndpoint.AddRoute(group);
        GetCardsEndpoint.AddRoute(group);
        GetCardEndpoint.AddRoute(group);
        UpdateCardEndpoint.AddRoute(group);
        DeleteCardEndpoint.AddRoute(group);
        GetNextAvailableVisitorCardEndpoint.AddRoute(group);
    }
}
