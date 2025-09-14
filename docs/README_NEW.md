# Housing Gate System — Consolidated Architecture & Data Model

 This document consolidates the original `docs/README.md` with detailed domain and data modeling guidance for entities in `src/LeafByte.Parking.API/Models/`. It is intended to be the source of truth for how we model residents, visitor flows, cards, devices, and access logging in an EF Core-backed .NET 9 API.

 ## Overview
 A system for managing access to residential gates using RFID cards. Residents get persistent cards; visitors are given temporary cards and their visit session is tracked. All access events are auditable and privacy-aware.

 ## Features (from original README)
 - RFID Card registration & deletion
 - Person data management (Resident & Non Resident)
 - RFID authentication
 - Access log management and reports
 - User management (2 levels)

 ## Security (from original README)
 - Data encryption for PII (name, NIK, phone, address)
 - Access authorization & user authentication
 - Input validation
 - Data backup & restore
 - Audit trail

 ## Technology
 - Backend: .NET 9 (C#)
   - Vertical Slice Architecture
   - Entity Framework Core
   - MediatR
   - FluentValidation
   - AutoMapper
   - Serilog
   - JWT
   - Minimal API
 - Frontend: Next.js, TailwindCSS, Shadcn UI
 - Database: PostgreSQL
 - Hardware: Arduino UNO + W5100 Shield + MFRC522 RFID Reader, IP Camera, PIR Motion Sensor

 ## Components (from original README)
 - Entry gate 1x, Exit gate 1x
 - RFID Reader 2x, RFID Cards ~100 pcs
 - Camera 1x, Motion Sensor 1x
 - Computer set 1x, Switch Hub (≥5 ports) 1x

 ## General Concepts (from original README)
 1. Manage house addresses (Block and House Number)
 2. Manage residents
 3. Manage visitors (issue temporary visitor cards)
 4. Manage RFID cards
 5. Manage RFID readers
 6. Manage cameras
 7. Manage motion sensors
 8. Manage gates
 9. Manage access logs
 10. Manage users
 11. Visitor identity (KTP/SIM/Passport) is collected on entry; a visitor card is issued and returned on exit when the ID is returned
 12. A resident can have more than one card and can freely enter/exit without showing ID

 ---

 # Domain Model Overview

 The model separates long-lived identities (Person, House) from short-lived visit sessions (VisitorInfo). Cards can be assigned to residents or used as temporary visitor cards.

 - House 1..* Person (residents)
 - Person 0..* Card
 - Card 0..* CardHistory
 - Card 0..* VisitorInfo (historical visits for visitor cards)
 - VisitorInfo 0..* Photo (evidence)
 - EntryLog references Card and Gates for every entry/exit event
 - Device represents infrastructure (RFID readers, cameras, etc.)

 Conventions and cross-cutting concerns:
 - Prefer `DateTimeOffset` for timestamps (timezone-safe). The codebase currently uses `DateTime` in several places; consider migrating for consistency.
 - Standard audit fields: `CreatedAt`, `UpdatedAt?`, and optionally `DeletedAt?` for soft delete.
 - Use `Status` where applicable (`Active`, `Inactive`, `Deleted`).
 - Initialize collections to avoid null navigation collections.
 - Consider `RowVersion` (concurrency token) on frequently updated rows such as `Card` and `VisitorInfo`.

 ---

 # Entity Reference and Recommendations

 The following recommendations align with current classes under `src/LeafByte.Parking.API/Models/` and extend them for robustness, privacy, and performance. Entity names below link to the existing files where applicable.

 ## Person (`Models/Person.cs`)
- Role: Long-lived identity record for residents (and optionally staff or known visitors).
- Current fields include: `Id`, `Name`, `ResidentType`, `IdentityType?`, `IdentityNumber?`, `Address?`, `Phone?`, `Gender?`, `Status`, `Notes?`, `HouseId?`, `CreatedAt`, `UpdatedAt?`, `DeletedAt?`.
- Recommendations:
  - Use `ResidentType` consistently (replacing any prior `PersonCategory` usage).
   - Prefer `Gender` enum instead of `char?` for clarity and validation.
   - Link to house: add `HouseId?` + `House?` navigation if residents must be linked to a house.
   - Initialize navigation: `ICollection<Card> Cards { get; set; } = new List<Card>();`
   - Use `DateTimeOffset` for timestamp fields.
   - Add soft delete where needed: `DeletedAt?`.
   - Index: unique when both identity fields are present — `(IdentityType, IdentityNumber)`.
   - Privacy: encrypt PII (Name, IdentityNumber, Phone, Address) via EF value converters.

 ## House (`Models/House.cs`)
 - Role: Address where residents live.
 - Current fields include: `Id`, `Block`, `Number`, `Notes`, `Status`, `CreatedAt`, `UpdatedAt?`, `DeletedAt?`, `ICollection<Person> Residents`.
 - Recommendations:
   - Initialize `Residents = new List<Person>();`
   - Unique index: `(Block, Number)`.
   - Use `DateTimeOffset` for timestamps.
   - Optionally track a primary resident: `OwnerId?` + `Person? Owner`.

 ## Card (`Models/Card.cs`)
 - Role: RFID card identity. Assigned to a `Person` if resident/service; used as a temporary token if visitor.
 - Current fields include: `Id (Guid)`, `CardType`, `CardUid`, `Label?`, `SequenceNumber?`, `PersonId?`, `VehicleTypeId?`, `VehicleNumber?`, `ValidFrom?`, `ValidTo?`, `Status`, `CreatedAt`, `UpdatedAt?`, `DeletedAt?`, `RowVersion?`, `History`, `Visits`.
 - Recommendations:
   - Ensure `CardUid` is stored in a normalized format (e.g., uppercase) and unique.
   - For resident/service cards, `PersonId` must be set; for visitor cards, it should be null (use `VisitorInfo` for session details).
   - Use `Label` and `SequenceNumber` to support visitor card pools (e.g., “Visitor 01..20”).
   - Use `ValidFrom/ValidTo` for service/merchant cards to control the access window.
   - Navigation collections: `ICollection<CardHistory> History`, `ICollection<VisitorInfo> Visits`.

 ## CardType (`Models/CardType.cs`)
 - Current values: `Resident`, `Visitor`, `Service`.
 - Recommendation: Expand as needed (e.g., `Staff`) if the domain requires distinct policy handling. If kept small and stable, enum is fine; otherwise consider reference data.

 ## CardHistory (`Models/CardHistory.cs`)
 - Role: Auditable trail of actions on a card (issued, reassigned, blocked, etc.).
 - Current fields include: `Id`, `CardId`, `DateTime`, `ActionType`, `Changes?`, `Notes?`, `CreatedAt`.
 - Recommendations:
   - Add namespace (if missing) and navigation: `Card Card`.
   - Rename `DateTime` to `OccurredAt` for clarity.
   - Optionally capture `UserId?` and/or `DeviceId?` to attribute actions.
   - Consider structured diffs: `ChangesJson`.
   - Index on `(CardId, OccurredAt)`.

 ## VisitorInfo (`Models/VisitorInfo.cs`)
 - Role: A visit/session record for a visitor using a card (usually a visitor card). Keeps identity snapshot and visit time window.
 - Current fields include: `Id`, `CardId`, `IdentityType?`, `IdentityNumber?`, `VisitorName?`, `VisitorPhone?`, `HostResidentName?`, `HostResidentId?`, `VisitPurpose?`, `VehicleType` (class), `VehicleNumber?`, `VisitStart`, `VisitEnd`, `Status`, `CreatedAt`, `UpdatedAt?`, `List<Photo> Photos`.
 - Recommendations:
   - Make `VisitEnd` nullable (open until checkout).
   - Add navigations: `Card`, `HostResident` (`Person`), optionally `HostHouse` (`House`).
   - Replace `VehicleType` instance with FK: `VehicleTypeId?` + `VehicleType?` navigation.
   - Initialize `Photos = new List<Photo>();`
   - Add `RowVersion` for concurrency.
   - Constraint: at most one open visit per `CardId` (enforce by app logic and/or filtered unique index on `(CardId, Status)` where open and `VisitEnd IS NULL`).

 ## EntryLog (`Models/EntryLog.cs`)
 - Role: Immutable log of entry/exit attempts.
 - Current fields include: `Id`, `CardId`, `EntryTime`, `EntryGateId`, `EntrySuccess`, `EntryError?`, `ExitTime?`, `ExitGateId?`, `ExitSuccess?`, `ExitError?`, `CreatedAt`, navigations for `Card`, `EntryGate`, `ExitGate`.
 - Recommendations:
   - Prefer `DateTimeOffset` for times.
   - Keep immutable; use append-only writes.
   - Indexes: `(CardId, EntryTime)` and `(EntryGateId, EntryTime)`.

 ## Gate (`Models/Gate.cs`)
 - Role: Physical gate definition (entry/exit).
 - Fields include: `Id`, `Name`, `Code`, `IsEntry`, `CreatedAt`, `UpdatedAt?`, `DeletedAt?`, `Status`.
 - Recommendations: Unique index on `Code`. Use `DateTimeOffset`.

 ## Device (`Models/Device.cs`)
 - Role: Infrastructure devices (RFID reader, camera, motion sensor, crossbar controller).
 - Fields include: `Id`, `Name`, `IpAddress`, `Type`, `Port`, timestamps, `Status`.
 - Recommendations: Unique index on `(Type, IpAddress, Port)`. Consider linking devices to a specific `Gate` if applicable.

 ## VehicleType (`Models/VehicleType.cs`)
 - Role: Reference data for vehicle categories.
 - Fields include: `Id`, `Name`, `Description`, `Status`, `CreatedAt`, `UpdatedAt?`, `DeletedAt?`.
 - Recommendations: Treat as reference data. Use FK references (`VehicleTypeId`) from `Card`/`VisitorInfo`.

 ## Photo (`Models/Photo.cs`)
 - Role: Evidence snapshots linked to visits or events.
 - Fields include: `Id`, `Path`, `Name`, `Extension`, `MimeType`, `Size`, `Description?`, `CreatedAt`.
 - Recommendations: Consider linking to `VisitorInfoId` or a generic ownership model if used elsewhere (e.g., `EntryLog`).

 ## Enums (`Models/Enums.cs`)
 - Currently defined: `UserRole`, `Status`, `HouseStatus`, `DeviceType`, `IdentityType`, `ResidentType`, `ActionType`.
 - Recommendations: Add `Gender` enum if you keep gender. Align `PersonCategory` with `ResidentType` to avoid duplication.

 ---

 # EF Core Modeling Notes

 Use Fluent API to centralize configuration (e.g., `Models/Configurations/*Configuration.cs`). Suggested configurations:

 - Card
   - `HasIndex(c => c.CardUid).IsUnique();`
   - `Property(c => c.CardUid).HasMaxLength(32);` (normalize to uppercase in code or via value converter)
   - `Property(c => c.RowVersion).IsRowVersion();`
   - Navigations: `HasMany(c => c.History)`, `HasMany(c => c.Visits)`.

 - House
   - `HasIndex(h => new { h.Block, h.Number }).IsUnique();`

 - Person
   - `HasIndex(p => new { p.IdentityType, p.IdentityNumber }).IsUnique()` with filter to allow nulls
   - `HasOne(p => p.House).WithMany(h => h.Residents).HasForeignKey(p => p.HouseId);`

 - VisitorInfo
   - `HasOne(v => v.Card).WithMany(c => c.Visits).HasForeignKey(v => v.CardId);`
   - `HasOne(v => v.HostResident).WithMany().HasForeignKey(v => v.HostResidentId);`
   - Optional filtered unique index for open visits per card.

 - CardHistory
   - `HasOne(h => h.Card).WithMany(c => c.History).HasForeignKey(h => h.CardId);`
   - `HasIndex(h => new { h.CardId, h.OccurredAt });`

 - EntryLog
   - `HasIndex(e => new { e.CardId, e.EntryTime });`
   - `HasOne(e => e.EntryGate).WithMany().HasForeignKey(e => e.EntryGateId).OnDelete(DeleteBehavior.Restrict);`
   - `HasOne(e => e.ExitGate).WithMany().HasForeignKey(e => e.ExitGateId).OnDelete(DeleteBehavior.Restrict);`

 - Value Converters for PII
   - For `Person.Name`, `Person.IdentityNumber`, `Person.Phone`, `Person.Address`: apply encryption value converters.
   - Keep crypto code centralized and configurable.

 - Timestamps
   - Prefer `DateTimeOffset` over `DateTime`. If staying with `DateTime`, store in UTC and enforce `DateTimeKind.Utc`.

 ---

 # Invariants and Validation Rules

 - Card
   - `CardUid` must be unique and normalized.
   - If `CardType == Resident`, `PersonId` must be set; if `CardType == Visitor`, `PersonId` should be null.

 - VisitorInfo
   - Exactly one open visit per `CardId` at a time (open = `VisitEnd == null` and active status).
   - `VisitStart <= VisitEnd` when closing a visit.

 - Person
   - If resident, must have a `HouseId`.
   - `(IdentityType, IdentityNumber)` pair unique when both not null.

 - House
   - `(Block, Number)` unique.

 - EntryLog
   - Append-only; no updates after write except for setting exit-related fields when exit occurs.

 These rules are enforced via FluentValidation in command handlers and complemented by EF Core constraints and database indexes.

 ---

 # Visitor Card Pool and Time-Bound Service Access

 ## Visitor Card Pool (predefined “Visitor 01..20”)
 - What: Maintain a pool of predefined visitor cards named in sequence (e.g., `Visitor 01..Visitor 20`).
 - Modeling:
   - `Card.CardType = Visitor`
   - `Card.Label = "Visitor 01"`, `Card.SequenceNumber = 1` (etc.)
   - `Card.PersonId = null`
   - Availability is derived: a card is available if it has no open `VisitorInfo`.
 - Business rules:
   - On check-in: pick the next available visitor card (lowest `SequenceNumber`), create `VisitorInfo` with `VisitStart` and `Status = Active`.
   - On check-out: set `VisitEnd`, `Status = Closed`, and return the card to the pool.
   - Do not issue a card that has an open visit.
 - Query example (EF Core):
```csharp
var nextCard = await db.Cards
  .Where(c => c.CardType == CardType.Visitor && c.Status == Status.Active)
  .Where(c => !db.Set<VisitorInfo>().Any(v => v.CardId == c.Id && v.Status == Status.Active && v.VisitEnd == null))
  .OrderBy(c => c.SequenceNumber)
  .FirstOrDefaultAsync();
```

 ## Service/Merchant/Supplier Access (time-bound)
 - What: Non-resident entities with repeated access over a period (e.g., 1 week, 2 weeks, 1 month).
 - Modeling (simple, recommended):
   - Create a non-resident `Person` (e.g., company contact).
   - Issue a `Card` with `CardType = Service`, assign `PersonId`.
   - Set `ValidFrom`/`ValidTo` to define the permitted access window.
   - Access is validated against the validity window; no `VisitorInfo` rows required per entry.
 - Enforcement:
   - Middleware/handler checks `ValidFrom <= now <= ValidTo`.
   - Scheduled task can inactivate cards past `ValidTo` and record a `CardHistory` entry.
 - Modeling (advanced, optional):
   - Introduce an `AccessPass` entity if you need richer constraints (allowed days, time windows, specific gates, entry quotas). The card acts as a token, the pass defines policy.

 ---

 # Migration Checklist

 1. Align `Person.PersonCategory` to `ResidentType` and update usages.
2. Replace `DateTime` with `DateTimeOffset` where feasible and update mappings.
3. Add missing navigations/FKs: `Card.Person`, `Card.VehicleTypeId`, `VisitorInfo.Card`, `VisitorInfo.HostResident`, etc.
4. Add suggested indexes and unique constraints:
   - `Card.CardUid` unique
   - `(House.Block, House.Number)` unique
   - `(Person.IdentityType, Person.IdentityNumber)` unique (filtered for nulls)
   - `(CardId, OccurredAt)` on `CardHistory`
   - `(CardType, Status)` on `Card` (for pool lookup)
   - Optional filtered unique index for one open visit per card
5. Introduce `RowVersion` where needed and configure via Fluent API.
6. Seed visitor card pool with `Label` and `SequenceNumber` (e.g., 20 cards named `Visitor 01..20`).
7. Add value converters for encrypted fields.
8. Create EF migrations and validate seed/reference data (e.g., `VehicleType`).

 ---

 # Future Extensions
 - Introduce `Staff` cards and access policies per role.
 - Add gate schedules and blackout periods.
 - Integrate ANPR (automatic number plate recognition) to verify `VehicleNumber`.
 - Extend `EntryLog` with device and camera correlation IDs for cross-system traceability.

 ---

 # Notes on Person vs VisitorInfo

 Keep them separate. `Person` is a long-lived identity; `VisitorInfo` is a short-lived visit/session tied to a card and a time window. Combining them complicates retention, constraints, and privacy. Optionally rename `VisitorInfo` to `VisitorVisit` for clarity, while still keeping it distinct from `Person`.