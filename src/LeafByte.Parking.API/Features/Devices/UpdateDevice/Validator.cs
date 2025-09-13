using FluentValidation;

namespace LeafByte.Parking.API.Features.Devices.UpdateDevice;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.IpAddress).NotEmpty().MaximumLength(15);
        RuleFor(x => x.Port).InclusiveBetween(1, 65535);
    }
}
