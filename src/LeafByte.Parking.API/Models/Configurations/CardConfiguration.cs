using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeafByte.Parking.API.Models.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CardUid)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(c => c.CardUid)
            .IsUnique();

        builder.Property(c => c.Label)
            .HasMaxLength(50);

        builder.HasIndex(c => new { c.CardType, c.Status, c.SequenceNumber });

        // Concurrency token
        builder.Property(c => c.RowVersion)
            .IsRowVersion();

        // Relationships
        builder.HasOne(c => c.Person)
            .WithMany(p => p.Cards)
            .HasForeignKey(c => c.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.History)
            .WithOne(h => h.Card)
            .HasForeignKey(h => h.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Visits)
            .WithOne(v => v.Card)
            .HasForeignKey(v => v.CardId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
