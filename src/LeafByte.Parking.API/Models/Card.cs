namespace LeafByte.Parking.API.Models;

public class Card
{
    public Guid Id { get; set; }
    public CardType CardType { get; set; }
    public required string CardUid { get; set; }

    // Human-friendly label for pools, e.g., "Visitor 01"
    public string? Label { get; set; }
    public int? SequenceNumber { get; set; }

    // Assignment for resident/service cards
    public int? PersonId { get; set; }
    public Person? Person { get; set; }

    // Optional vehicle association
    public int? VehicleTypeId { get; set; }
    public VehicleType? VehicleType { get; set; }
    public string? VehicleNumber { get; set; }

    // Validity window (replaces generic DueDate)
    public DateTimeOffset? ValidFrom { get; set; }
    public DateTimeOffset? ValidTo { get; set; }

    public Status Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    // Concurrency token
    public byte[]? RowVersion { get; set; }

    // Navigations
    public ICollection<CardHistory> History { get; set; } = new List<CardHistory>();
    public ICollection<VisitorInfo> Visits { get; set; } = new List<VisitorInfo>();
}
