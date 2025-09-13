using MediatR;
using LeafByte.Parking.API.Data;
using LeafByte.Parking.API.Models;
using LeafByte.Parking.CrossCutting.Exceptions;

namespace LeafByte.Parking.API.Features.Users.UpdateUser;

public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Response>
{
    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(new object[] { request.Id }, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException($"User with id {request.Id} not found");
        }

        user.Username = request.Username;
        user.Role = request.Role;
        user.Status = request.Status;
        user.UpdatedAt = DateTime.UtcNow;

        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);

        return new Response(true);
    }
}
