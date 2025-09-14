namespace LeafByte.Parking.API.Models;

public class CardHistory
{
    public int Id { get; set; }

    public Guid CardId { get; set; }
    public Card Card { get; set; } = null!;

    public DateTimeOffset OccurredAt { get; set; }
    public ActionType ActionType { get; set; }
    public string? ChangesJson { get; set; }
    public string? Notes { get; set; }

    public int? UserId { get; set; }
    public User? User { get; set; }

    public int? DeviceId { get; set; }
    public Device? Device { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}