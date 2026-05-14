using Core.Domain.Primitives;

namespace CourtOwners.Application.CourtOwners;

public sealed class CourtOwnerReadModel : IProjection
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PasswordHash { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string Document { get; init; } = string.Empty;
    public bool IsDeleted { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}
