namespace LeafByte.Parking.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Person> Persons { get; set; } = default!;
    public DbSet<Card> Cards { get; set; } = default!;
    public DbSet<Gate> Gates { get; set; } = default!;
    public DbSet<EntryLog> EntryLogs { get; set; } = default!;
    public DbSet<Device> Devices { get; set; } = default!;
    public DbSet<VisitorInfo> VisitorInfos { get; set; } = default!;
    public DbSet<House> Houses { get; set; } = default!;
    public DbSet<VehicleType> VehicleTypes { get; set; } = default!;
    public DbSet<Photo> Photos { get; set; } = default!;
    public DbSet<CardHistory> CardHistories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations from Models/Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}