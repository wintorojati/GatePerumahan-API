namespace LeafByte.Parking.API.Models;

public class Gate
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Status Status { get; set; }
}
