using FluentValidation;

namespace LeafByte.Parking.API.Features.EntryLogs.CreateEntryLog;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.CardId).NotEmpty();
        RuleFor(x => x.EntryGateId).GreaterThan(0);
        RuleFor(x => x.EntryError).MaximumLength(500).When(x => x.EntryError != null);
    }
}
