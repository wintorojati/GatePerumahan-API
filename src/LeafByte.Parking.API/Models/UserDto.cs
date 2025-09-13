namespace LeafByte.Parking.API.Models;

public class UserDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public Status Status { get; set; }
}
