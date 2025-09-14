using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;
using LeafByte.Parking.CrossCutting.Pagination;

namespace LeafByte.Parking.API.Features.Persons.GetPersons;

public class Handler : IRequestHandler<Query, PaginatedResult<PersonDto>>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<PersonDto>> Handle(Query request, CancellationToken cancellationToken)
    {
        var query = _context.Persons
            .Where(p => p.Status == Status.Active)
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var paginatedQuery = query
            .Skip(request.Pagination.PageIndex * request.Pagination.PageSize)
            .Take(request.Pagination.PageSize);

        var persons = await paginatedQuery
            .Select(p => new PersonDto(
                p.Id,
                p.Name,
                p.ResidentType,
                p.Gender,
                p.IdentityType,
                p.IdentityNumber,
                p.Address,
                p.Phone,
                p.HouseId,
                p.CreatedAt))
            .ToListAsync(cancellationToken);

        return new PaginatedResult<PersonDto>(
            request.Pagination.PageIndex,
            request.Pagination.PageSize,
            totalCount,
            persons);
    }
}
