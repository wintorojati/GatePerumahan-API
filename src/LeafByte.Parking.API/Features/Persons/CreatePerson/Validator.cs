using FluentValidation;

namespace LeafByte.Parking.API.Features.Persons.CreatePerson;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Gender).Must(g => g == 'M' || g == 'F').WithMessage("Gender must be 'M' or 'F'");
        RuleFor(x => x.Nik).NotEmpty().Length(16).WithMessage("NIK must be 16 characters");
        RuleFor(x => x.Address).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(20);
        RuleFor(x => x.LicensePlateNumber).NotEmpty().MaximumLength(10);
    }
}
