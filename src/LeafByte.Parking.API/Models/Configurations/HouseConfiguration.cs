using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeafByte.Parking.API.Models.Configurations;

public class HouseConfiguration : IEntityTypeConfiguration<House>
{
    public void Configure(EntityTypeBuilder<House> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Block).IsRequired().HasMaxLength(50);
        builder.Property(h => h.Number).IsRequired().HasMaxLength(50);
        builder.HasIndex(h => new { h.Block, h.Number }).IsUnique();

        builder.HasMany(h => h.Residents)
            .WithOne(p => p.House)
            .HasForeignKey(p => p.HouseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
