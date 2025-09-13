using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.EntryLogs.CreateEntryLog;

public class Handler : IRequestHandler<Command, Response>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var entryLog = new EntryLog
        {
            CardId = request.CardId,
            EntryTime = request.EntryTime,
            EntryGateId = request.EntryGateId,
            EntrySuccess = request.EntrySuccess,
            EntryError = request.EntryError,
            CreatedAt = DateTime.UtcNow
        };

        _context.EntryLogs.Add(entryLog);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(entryLog.Id);
    }
}
