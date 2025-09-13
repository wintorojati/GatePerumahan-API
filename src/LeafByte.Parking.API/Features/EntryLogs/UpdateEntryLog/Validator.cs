using FluentValidation;

namespace LeafByte.Parking.API.Features.EntryLogs.UpdateEntryLog;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.ExitGateId).GreaterThan(0).When(x => x.ExitGateId.HasValue);
        RuleFor(x => x.ExitError).MaximumLength(500).When(x => x.ExitError != null);
    }
}
