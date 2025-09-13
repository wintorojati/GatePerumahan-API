using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LeafByte.Parking.API.Features.EntryLogs.GetEntryLog;

public class Handler : IRequestHandler<Query, Response?>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response?> Handle(Query request, CancellationToken cancellationToken)
    {
        var entryLog = await _context.EntryLogs
            .AsNoTracking()
            .FirstOrDefaultAsync(el => el.Id == request.Id, cancellationToken);

        if (entryLog is null)
            return null;

        return new Response(
            entryLog.Id,
            entryLog.CardId,
            entryLog.EntryTime,
            entryLog.EntryGateId,
            entryLog.EntrySuccess,
            entryLog.EntryError,
            entryLog.ExitTime,
            entryLog.ExitGateId,
            entryLog.ExitSuccess,
            entryLog.ExitError,
            entryLog.CreatedAt);
    }
}
