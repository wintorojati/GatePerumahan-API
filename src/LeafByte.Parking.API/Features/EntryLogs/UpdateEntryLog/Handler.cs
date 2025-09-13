using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LeafByte.Parking.API.Features.EntryLogs.UpdateEntryLog;

public class Handler : IRequestHandler<Command>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(Command request, CancellationToken cancellationToken)
    {
        var entryLog = await _context.EntryLogs
            .FirstOrDefaultAsync(el => el.Id == request.Id, cancellationToken);

        if (entryLog is null)
            return;

        entryLog.ExitTime = request.ExitTime;
        entryLog.ExitGateId = request.ExitGateId;
        entryLog.ExitSuccess = request.ExitSuccess;
        entryLog.ExitError = request.ExitError;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
