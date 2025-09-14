namespace LeafByte.Parking.API.Models;

public class Photo
{
    public Guid Id { get; set; }

    public string Path { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public string MimeType { get; set; }
    public long Size { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}