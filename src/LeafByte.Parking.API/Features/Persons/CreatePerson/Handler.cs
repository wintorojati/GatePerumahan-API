using MediatR;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Persons.CreatePerson;

public class Handler : IRequestHandler<Command, Response>
{
    private readonly ApplicationDbContext _context;

    public Handler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var person = new Person
        {
            Name = request.Name,
            ResidentType = request.ResidentType,
            Gender = request.Gender,
            IdentityType = request.IdentityType,
            IdentityNumber = request.IdentityNumber,
            Address = request.Address,
            Phone = request.Phone,
            HouseId = request.HouseId,
            CreatedAt = DateTimeOffset.UtcNow,
            Status = Status.Active
        };

        _context.Persons.Add(person);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(person.Id);
    }
}
