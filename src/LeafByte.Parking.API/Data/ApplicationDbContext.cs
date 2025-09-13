namespace LeafByte.Parking.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Person> Persons { get; set; } = default!;
    public DbSet<Card> Cards { get; set; } = default!;
    public DbSet<Gate> Gates { get; set; } = default!;
    public DbSet<EntryLog> EntryLogs { get; set; } = default!;
    public DbSet<Device> Devices { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships if needed
        modelBuilder.Entity<Card>()
            .HasOne(c => c.Person)
            .WithMany()
            .HasForeignKey(c => c.PersonId);

        modelBuilder.Entity<EntryLog>()
            .HasOne(e => e.Card)
            .WithMany()
            .HasForeignKey(e => e.CardId);

        modelBuilder.Entity<EntryLog>()
            .HasOne(e => e.EntryGate)
            .WithMany()
            .HasForeignKey(e => e.EntryGateId);

        modelBuilder.Entity<EntryLog>()
            .HasOne(e => e.ExitGate)
            .WithMany()
            .HasForeignKey(e => e.ExitGateId);
    }
}