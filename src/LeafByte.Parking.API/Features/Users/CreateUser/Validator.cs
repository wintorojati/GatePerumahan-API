using FluentValidation;

namespace LeafByte.Parking.API.Features.Users.CreateUser;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.Role)
            .IsInEnum();
    }
}
