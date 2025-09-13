using FluentValidation;

namespace LeafByte.Parking.API.Features.Users.UpdateUser;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
        RuleFor(x => x.Role).IsInEnum();
        RuleFor(x => x.Status).IsInEnum();
    }
}
