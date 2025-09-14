using FluentValidation;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Cards.CreateCard;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.PersonId)
            .NotNull()
            .GreaterThan(0)
            .When(x => x.CardType == CardType.Resident || x.CardType == CardType.Service)
            .WithMessage("PersonId is required for Resident and Service cards.");
        RuleFor(x => x.PersonId)
            .Must(pid => pid == null)
            .When(x => x.CardType == CardType.Visitor)
            .WithMessage("PersonId must be null for Visitor cards.");
        RuleFor(x => x.CardUid).NotEmpty().MaximumLength(50);
        RuleFor(x => x.CardType).IsInEnum();
    }
}
