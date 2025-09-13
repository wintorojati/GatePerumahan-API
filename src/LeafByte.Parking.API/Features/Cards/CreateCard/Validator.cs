using FluentValidation;

namespace LeafByte.Parking.API.Features.Cards.CreateCard;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.PersonId).GreaterThan(0);
        RuleFor(x => x.CardUid).NotEmpty().MaximumLength(50);
        RuleFor(x => x.AccessType).IsInEnum();
    }
}
