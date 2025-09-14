using FluentValidation;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Persons.CreatePerson;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ResidentType).IsInEnum();
        RuleFor(x => x.Gender).IsInEnum().When(x => x.Gender.HasValue);
        RuleFor(x => x.IdentityType).IsInEnum().When(x => x.IdentityType.HasValue);
        RuleFor(x => x.IdentityNumber).MaximumLength(32).When(x => x.IdentityNumber != null);
        RuleFor(x => x.Address).MaximumLength(200).When(x => x.Address != null);
        RuleFor(x => x.Phone).MaximumLength(20).When(x => x.Phone != null);
    }
}
