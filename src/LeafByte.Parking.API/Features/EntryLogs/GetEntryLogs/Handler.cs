using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;
using LeafByte.Parking.CrossCutting.Pagination;

namespace LeafByte.Parking.API.Features.EntryLogs.GetEntryLogs;

public class Handler : IRequestHandler<Query, PaginatedResult<EntryLogDto>>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<EntryLogDto>> Handle(Query request, CancellationToken cancellationToken)
    {
        var query = _context.EntryLogs
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var paginatedQuery = query
            .Skip(request.Pagination.PageIndex * request.Pagination.PageSize)
            .Take(request.Pagination.PageSize);

        var entryLogs = await paginatedQuery
            .Select(el => new EntryLogDto(
                el.Id,
                el.CardId,
                el.EntryTime,
                el.EntryGateId,
                el.EntrySuccess,
                el.ExitTime,
                el.ExitGateId,
                el.ExitSuccess,
                el.CreatedAt))
            .ToListAsync(cancellationToken);

        return new PaginatedResult<EntryLogDto>(
            request.Pagination.PageIndex,
            request.Pagination.PageSize,
            totalCount,
            entryLogs);
    }
}
