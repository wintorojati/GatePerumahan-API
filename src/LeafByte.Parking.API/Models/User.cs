namespace LeafByte.Parking.API.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Salt { get; set; }
    public UserRole Role { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Status Status { get; set; }
}
