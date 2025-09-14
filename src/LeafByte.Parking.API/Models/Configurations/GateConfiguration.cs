using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeafByte.Parking.API.Models.Configurations;

public class GateConfiguration : IEntityTypeConfiguration<Gate>
{
    public void Configure(EntityTypeBuilder<Gate> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired();

        builder.Property(g => g.Code)
            .IsRequired();

        builder.HasIndex(g => g.Code)
            .IsUnique();
    }
}
