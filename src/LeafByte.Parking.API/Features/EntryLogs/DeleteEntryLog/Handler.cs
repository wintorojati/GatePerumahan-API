using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LeafByte.Parking.API.Features.EntryLogs.DeleteEntryLog;

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

        _context.EntryLogs.Remove(entryLog);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
