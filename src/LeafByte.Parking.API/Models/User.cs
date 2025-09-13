namespace LeafByte.Parking.API.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Salt { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Status Status { get; set; }
}
