using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;
using LeafByte.Parking.CrossCutting.Pagination;

namespace LeafByte.Parking.API.Features.Gates.GetGates;

public class Handler : IRequestHandler<Query, PaginatedResult<GateDto>>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<GateDto>> Handle(Query request, CancellationToken cancellationToken)
    {
        var query = _context.Gates
            .Where(g => g.Status == Status.Active)
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var paginatedQuery = query
            .Skip(request.Pagination.PageIndex * request.Pagination.PageSize)
            .Take(request.Pagination.PageSize);

        var gates = await paginatedQuery
            .Select(g => new GateDto(
                g.Id,
                g.Name,
                g.Code,
                g.CreatedAt,
                g.UpdatedAt))
            .ToListAsync(cancellationToken);

        return new PaginatedResult<GateDto>(
            request.Pagination.PageIndex,
            request.Pagination.PageSize,
            totalCount,
            gates);
    }
}
