namespace LeafByte.Parking.API.Models;

public class VehicleType
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public Status Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}