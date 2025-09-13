using MediatR;

namespace LeafByte.Parking.API.Features.EntryLogs.UpdateEntryLog;

public record Command(
    int Id,
    DateTime? ExitTime,
    int? ExitGateId,
    bool? ExitSuccess,
    string? ExitError) : IRequest;
