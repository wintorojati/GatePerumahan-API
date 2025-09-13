namespace LeafByte.Parking.API.Models;

public class Card
{
    public Guid Id { get; set; }
    public int PersonId { get; set; }
    public required string CardUid { get; set; }
    public AccessType AccessType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Status Status { get; set; }

    // Navigation property
    public Person? Person { get; set; }
}
