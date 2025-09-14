using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeafByte.Parking.API.Models.Configurations;

public class VisitorInfoConfiguration : IEntityTypeConfiguration<VisitorInfo>
{
    public void Configure(EntityTypeBuilder<VisitorInfo> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.VisitStart).IsRequired();

        // One open visit per card (Status = Active (0) and VisitEnd IS NULL)
        builder.HasIndex(v => new { v.CardId, v.Status })
            .HasFilter("\"VisitEnd\" IS NULL AND \"Status\" = 0");

        builder.HasOne(v => v.Card)
            .WithMany(c => c.Visits)
            .HasForeignKey(v => v.CardId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.HostResident)
            .WithMany()
            .HasForeignKey(v => v.HostResidentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.VehicleType)
            .WithMany()
            .HasForeignKey(v => v.VehicleTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(v => v.RowVersion)
            .IsRowVersion();
    }
}
