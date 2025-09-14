using MediatR;
using System;

namespace LeafByte.Parking.API.Features.EntryLogs.GetEntryLog;

public record Query(int Id) : IRequest<Response?>;

public record Response(
    int Id,
    Guid CardId,
    DateTimeOffset EntryTime,
    int EntryGateId,
    bool EntrySuccess,
    string? EntryError,
    DateTimeOffset? ExitTime,
    int? ExitGateId,
    bool? ExitSuccess,
    string? ExitError,
    DateTimeOffset CreatedAt);
