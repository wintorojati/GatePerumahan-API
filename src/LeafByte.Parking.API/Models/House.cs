namespace LeafByte.Parking.API.Models;

public class House
{
    public int Id {get; set;}
    public required string Block {get; set;}  
    public required string Number {get; set;}
    public string? Notes {get; set;}
    public HouseStatus Status {get; set;}

    public DateTimeOffset CreatedAt {get; set;}
    public DateTimeOffset? UpdatedAt {get; set;}
    public DateTimeOffset? DeletedAt {get; set;}

    public virtual ICollection<Person> Residents {get; set;} = new List<Person>();
}