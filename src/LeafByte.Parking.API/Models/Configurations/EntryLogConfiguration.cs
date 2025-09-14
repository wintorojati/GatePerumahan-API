using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeafByte.Parking.API.Models.Configurations;

public class EntryLogConfiguration : IEntityTypeConfiguration<EntryLog>
{
    public void Configure(EntityTypeBuilder<EntryLog> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => new { e.CardId, e.EntryTime });
        builder.HasIndex(e => new { e.EntryGateId, e.EntryTime });

        builder.HasOne(e => e.Card)
            .WithMany()
            .HasForeignKey(e => e.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.EntryGate)
            .WithMany()
            .HasForeignKey(e => e.EntryGateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.ExitGate)
            .WithMany()
            .HasForeignKey(e => e.ExitGateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
