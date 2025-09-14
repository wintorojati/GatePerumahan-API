namespace LeafByte.Parking.API.Models;

public class Gate
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public bool IsEntry { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Status Status { get; set; }
}
