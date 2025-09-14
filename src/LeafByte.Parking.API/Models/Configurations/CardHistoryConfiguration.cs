using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeafByte.Parking.API.Models.Configurations;

public class CardHistoryConfiguration : IEntityTypeConfiguration<CardHistory>
{
    public void Configure(EntityTypeBuilder<CardHistory> builder)
    {
        builder.HasKey(h => h.Id);

        builder.HasIndex(h => new { h.CardId, h.OccurredAt });

        builder.Property(h => h.ChangesJson)
            .HasMaxLength(2000);

        builder.HasOne(h => h.Card)
            .WithMany(c => c.History)
            .HasForeignKey(h => h.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.User)
            .WithMany()
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(h => h.Device)
            .WithMany()
            .HasForeignKey(h => h.DeviceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
