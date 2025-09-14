using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeafByte.Parking.API.Models.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired();

        builder.Property(d => d.IpAddress)
            .IsRequired();

        builder.HasIndex(d => new { d.Type, d.IpAddress, d.Port })
            .IsUnique();
    }
}
