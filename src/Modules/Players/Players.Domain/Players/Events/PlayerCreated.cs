using Core.Domain.Primitives;

namespace Players.Domain.Players.Events;

public sealed record PlayerCreated(
    Guid PlayerId,
    string Name,
    string Email,
    string PasswordHash,
    string Phone,
    ulong Version) : Event, IDomainEvent;
