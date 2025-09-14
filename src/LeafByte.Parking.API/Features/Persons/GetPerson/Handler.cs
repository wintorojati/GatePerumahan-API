using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LeafByte.Parking.API.Features.Persons.GetPerson;

public class Handler : IRequestHandler<Query, Response>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response?> Handle(Query request, CancellationToken cancellationToken)
    {
        var person = await _context.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.Id && p.Status == Status.Active, cancellationToken);

        if (person is null)
            return null;

        return new Response(
            person.Id,
            person.Name,
            person.ResidentType,
            person.Gender,
            person.IdentityType,
            person.IdentityNumber,
            person.Address,
            person.Phone,
            person.HouseId,
            person.CreatedAt,
            person.UpdatedAt,
            person.Status);
    }
}
