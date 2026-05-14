using Core.Domain.Primitives;

namespace CourtOwners.Domain.CourtOwners.Events;

public sealed record CourtOwnerCreated(
    Guid CourtOwnerId,
    string Name,
    string Email,
    string PasswordHash,
    string Phone,
    string Document,
    ulong Version) : Event, IDomainEvent;
