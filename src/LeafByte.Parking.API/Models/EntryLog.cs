namespace LeafByte.Parking.API.Models;

public class EntryLog
{
    public int Id { get; set; }
    public Guid CardId { get; set; }
    public DateTime EntryTime { get; set; }
    public int EntryGateId { get; set; }
    public bool EntrySuccess { get; set; }
    public string? EntryError { get; set; }
    public DateTime? ExitTime { get; set; }
    public int? ExitGateId { get; set; }
    public bool? ExitSuccess { get; set; }
    public string? ExitError { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Card? Card { get; set; }
    public Gate? EntryGate { get; set; }
    public Gate? ExitGate { get; set; }
}
