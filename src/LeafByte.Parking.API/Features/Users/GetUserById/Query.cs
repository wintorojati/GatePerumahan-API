using MediatR;

namespace LeafByte.Parking.API.Features.Users.GetUserById;

public record Query(int Id) : IRequest<Response>;

public record Response(UserDto User);

public class UserDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public Status Status { get; set; }
}
