using MediatR;

namespace LeafByte.Parking.API.Features.Cards.DeleteCard;

public record Command(Guid Id) : IRequest;
