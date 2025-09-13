using MediatR;

namespace LeafByte.Parking.API.Features.EntryLogs.GetEntryLog;

public record Query(int Id) : IRequest<Response?>;

public record Response(
    int Id,
    Guid CardId,
    DateTime EntryTime,
    int EntryGateId,
    bool EntrySuccess,
    string? EntryError,
    DateTime? ExitTime,
    int? ExitGateId,
    bool? ExitSuccess,
    string? ExitError,
    DateTime CreatedAt);
