using LeafByte.Parking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LeafByte.Parking.API.Data.Seed;

public static class VisitorCardSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db, IConfiguration configuration, ILogger logger, CancellationToken ct = default)
    {
        var section = configuration.GetSection("VisitorPool:Cards");
        if (!section.Exists())
        {
            logger.LogInformation("VisitorPool:Cards not configured; skipping visitor card seeding.");
            return;
        }

        // Each entry should have: CardUid, Label, SequenceNumber
        var entries = section.GetChildren().Select(c => new
        {
            CardUid = c["CardUid"],
            Label = c["Label"],
            SequenceNumber = int.TryParse(c["SequenceNumber"], out var n) ? n : (int?)null
        }).Where(x => !string.IsNullOrWhiteSpace(x.CardUid)).ToList();

        if (entries.Count == 0)
        {
            logger.LogInformation("VisitorPool:Cards configured but empty.");
            return;
        }

        foreach (var e in entries)
        {
            var uid = e.CardUid!.Trim().ToUpperInvariant();
            var exists = await db.Cards.AsNoTracking().AnyAsync(c => c.CardUid == uid, ct);
            if (exists)
                continue;

            var card = new Card
            {
                Id = Guid.NewGuid(),
                CardUid = uid,
                CardType = CardType.Visitor,
                Label = e.Label,
                SequenceNumber = e.SequenceNumber,
                Status = Status.Active,
                CreatedAt = DateTimeOffset.UtcNow
            };

            db.Cards.Add(card);
        }

        if (db.ChangeTracker.HasChanges())
        {
            await db.SaveChangesAsync(ct);
            logger.LogInformation("Seeded visitor card pool: {Count} new cards added.", entries.Count);
        }
    }
}
