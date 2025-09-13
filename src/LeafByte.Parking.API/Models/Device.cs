namespace LeafByte.Parking.API.Models;

public class Device
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string IpAddress { get; set; }
    public DeviceType Type { get; set; }
    public int Port { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Status Status { get; set; }
}
