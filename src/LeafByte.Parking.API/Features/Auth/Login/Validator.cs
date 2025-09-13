using FluentValidation;

namespace LeafByte.Parking.API.Features.Auth.Login;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(100);
    }
}
