using MediatR;
using LeafByte.Parking.CrossCutting.Pagination;

namespace LeafByte.Parking.API.Features.EntryLogs.GetEntryLogs;

public record Query(PaginationRequest Pagination) : IRequest<PaginatedResult<EntryLogDto>>;

public record EntryLogDto(
    int Id,
    Guid CardId,
    DateTime EntryTime,
    int EntryGateId,
    bool EntrySuccess,
    DateTime? ExitTime,
    int? ExitGateId,
    bool? ExitSuccess,
    DateTime CreatedAt);
