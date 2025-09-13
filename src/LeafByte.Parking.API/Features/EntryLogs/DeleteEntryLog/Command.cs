using MediatR;

namespace LeafByte.Parking.API.Features.EntryLogs.DeleteEntryLog;

public record Command(int Id) : IRequest;
