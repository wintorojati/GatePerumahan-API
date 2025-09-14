namespace LeafByte.Parking.API.Models;

/// <summary>
/// Visitor Info if visitor is not resident, collect identity when entry
/// </summary>
public class VisitorInfo
{
    public int Id { get; set; }
    public Guid CardId { get; set; }
    public Card Card { get; set; } = null!;
    public IdentityType? IdentityType { get; set; }
    public string? IdentityNumber { get; set; }
    public string? VisitorName { get; set; }
    public string? VisitorPhone { get; set; }
    public string? HostResidentName { get; set; }
    public int? HostResidentId { get; set; }
    public Person? HostResident { get; set; }
    public string? VisitPurpose { get; set; }
    public int? VehicleTypeId { get; set; }
    public VehicleType? VehicleType { get; set; }
    public string? VehicleNumber { get; set; }
    public DateTimeOffset VisitStart { get; set; }
    public DateTimeOffset? VisitEnd { get; set; }
    public Status Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public byte[]? RowVersion { get; set; }
    public List<Photo> Photos { get; set; } = new();
}
