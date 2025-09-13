using Carter;
using Microsoft.AspNetCore.Authorization;
using LeafByte.Parking.API.Features.Cards.CreateCard;
using LeafByte.Parking.API.Features.Cards.GetCards;
using LeafByte.Parking.API.Features.Cards.GetCard;
using LeafByte.Parking.API.Features.Cards.UpdateCard;
using LeafByte.Parking.API.Features.Cards.DeleteCard;

namespace LeafByte.Parking.API.Features.Cards;

public static class CardEndPoints
{
    public static void AddCardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/cards").WithTags("Cards").RequireAuthorization();

        group.MapPost("/", CreateCardEndpoint.AddRoute);
        group.MapGet("/", GetCardsEndpoint.AddRoute);
        group.MapGet("/{id}", GetCardEndpoint.AddRoute);
        group.MapPut("/{id}", UpdateCardEndpoint.AddRoute);
        group.MapDelete("/{id}", DeleteCardEndpoint.AddRoute);
    }
}
