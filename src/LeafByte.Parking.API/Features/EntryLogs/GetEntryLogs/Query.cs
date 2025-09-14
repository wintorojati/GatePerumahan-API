using MediatR;
using LeafByte.Parking.CrossCutting.Pagination;
using System;

namespace LeafByte.Parking.API.Features.EntryLogs.GetEntryLogs;

public record Query(PaginationRequest Pagination) : IRequest<PaginatedResult<EntryLogDto>>;

public record EntryLogDto(
    int Id,
    Guid CardId,
    DateTimeOffset EntryTime,
    int EntryGateId,
    bool EntrySuccess,
    DateTimeOffset? ExitTime,
    int? ExitGateId,
    bool? ExitSuccess,
    DateTimeOffset CreatedAt);
