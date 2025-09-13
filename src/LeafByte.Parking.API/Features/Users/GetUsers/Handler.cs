using AutoMapper;
using LeafByte.Parking.API.Features.Users.GetUserById;
using LeafByte.Parking.CrossCutting.Pagination;
using MediatR;

namespace LeafByte.Parking.API.Features.Users.GetUsers;

public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Response>
{
    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        var pageIndex = request.Request.PageIndex;
        var pageSize = request.Request.PageSize;

        var totalCount = await context.Users.LongCountAsync(cancellationToken);

        var users = await context.Users
            .OrderBy(u => u.Username)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var userDtos = mapper.Map<List<UserDto>>(users);

        var result = new PaginatedResult<UserDto>(pageIndex, pageSize, totalCount, userDtos);

        return new Response(result);
    }
}
