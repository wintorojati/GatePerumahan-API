namespace LeafByte.Parking.API.Models;

public class Photo
{
    public Guid Id { get; set; }

    public required string Path { get; set; }
    public required string Name { get; set; }
    public required string Extension { get; set; }
    public required string MimeType { get; set; }
    public long Size { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}