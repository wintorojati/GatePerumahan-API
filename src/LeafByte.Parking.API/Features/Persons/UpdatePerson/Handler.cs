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
        person.Gender = request.Gender;
        person.Nik = request.Nik;
        person.Address = request.Address;
        person.Phone = request.Phone;
        person.LicensePlateNumber = request.LicensePlateNumber;
        person.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
