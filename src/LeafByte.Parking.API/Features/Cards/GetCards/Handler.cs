using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;
using LeafByte.Parking.CrossCutting.Pagination;

namespace LeafByte.Parking.API.Features.Cards.GetCards;

public class Handler : IRequestHandler<Query, PaginatedResult<CardDto>>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<CardDto>> Handle(Query request, CancellationToken cancellationToken)
    {
        var query = _context.Cards
            .Where(c => c.Status == Status.Active)
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var paginatedQuery = query
            .Skip(request.Pagination.PageIndex * request.Pagination.PageSize)
            .Take(request.Pagination.PageSize);

        var cards = await paginatedQuery
            .Select(c => new CardDto(
                c.Id,
                c.PersonId,
                c.CardUid,
                c.CardType,
                c.CreatedAt,
                c.Status))
            .ToListAsync(cancellationToken);

        return new PaginatedResult<CardDto>(
            request.Pagination.PageIndex,
            request.Pagination.PageSize,
            totalCount,
            cards);
    }
}
