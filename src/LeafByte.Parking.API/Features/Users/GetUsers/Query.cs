using MediatR;
using LeafByte.Parking.CrossCutting.Pagination;
using LeafByte.Parking.API.Models;

namespace LeafByte.Parking.API.Features.Users.GetUsers;

public record Query(PaginationRequest Request) : IRequest<Response>;

public record Response(PaginatedResult<UserDto> Result);
