using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeafByte.Parking.API.Models.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.IdentityNumber)
            .HasMaxLength(32);

        builder.Property(p => p.Phone)
            .HasMaxLength(20);

        builder.Property(p => p.Address)
            .HasMaxLength(200);

        builder.HasIndex(p => new { p.IdentityType, p.IdentityNumber })
            .HasFilter("\"IdentityType\" IS NOT NULL AND \"IdentityNumber\" IS NOT NULL")
            .IsUnique();

        builder.HasOne(p => p.House)
            .WithMany(h => h.Residents)
            .HasForeignKey(p => p.HouseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
