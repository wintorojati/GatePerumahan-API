namespace LeafByte.Parking.API.Models;

public class Person
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required ResidentType ResidentType { get; set; }
    public IdentityType? IdentityType { get; set; }
    public string? IdentityNumber { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public Gender? Gender { get; set; }
    public Status Status { get; set; }
    public string? Notes {get; set;}

    public int? HouseId { get; set; }
    public House? House { get; set; }

    public ICollection<Card> Cards { get; set; } = new List<Card>();

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
