namespace LeafByte.Parking.API.Models;

public class Person
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public char Gender { get; set; }
    public required string Nik { get; set; }
    public required string Address { get; set; }
    public required string Phone { get; set; }
    public required string LicensePlateNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Status Status { get; set; }
}
