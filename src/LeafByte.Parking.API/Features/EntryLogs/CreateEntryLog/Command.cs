using MediatR;

namespace LeafByte.Parking.API.Features.EntryLogs.CreateEntryLog;

public record Command(
    Guid CardId,
    DateTime EntryTime,
    int EntryGateId,
    bool EntrySuccess,
    string? EntryError) : IRequest<Response>;

public record Response(int Id);
