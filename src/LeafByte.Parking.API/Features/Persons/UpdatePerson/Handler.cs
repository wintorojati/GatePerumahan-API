using MediatR;
using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LeafByte.Parking.API.Features.Persons.UpdatePerson;

public class Handler : IRequestHandler<Command>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(Command request, CancellationToken cancellationToken)
    {
        var person = await _context.Persons
            .FirstOrDefaultAsync(p => p.Id == request.Id && p.Status == Status.Active, cancellationToken);

        if (person is null)
            return;

        person.Name = request.Name;
        person.ResidentType = request.ResidentType;
        person.Gender = request.Gender;
        person.IdentityType = request.IdentityType;
        person.IdentityNumber = request.IdentityNumber;
        person.Address = request.Address;
        person.Phone = request.Phone;
        person.HouseId = request.HouseId;
        person.UpdatedAt = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
