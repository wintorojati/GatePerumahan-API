using FluentValidation;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Cards.UpdateCard;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.PersonId).GreaterThan(0);
        RuleFor(x => x.CardUid).NotEmpty().MaximumLength(50);
        RuleFor(x => x.CardType).IsInEnum();
    }
}
