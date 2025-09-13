using MediatR;
using LeafByte.Parking.API.Data;
using LeafByte.Parking.API.Models;
using LeafByte.Parking.CrossCutting.Exceptions;
using AutoMapper;

namespace LeafByte.Parking.API.Features.Users.GetUserById;

public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Response>
{
    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(new object[] { request.Id }, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException($"User with id {request.Id} not found");
        }

        var userDto = mapper.Map<UserDto>(user);

        return new Response(userDto);
    }
}
