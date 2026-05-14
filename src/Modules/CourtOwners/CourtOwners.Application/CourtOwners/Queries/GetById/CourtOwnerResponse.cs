namespace CourtOwners.Application.CourtOwners.Queries.GetById;

public sealed record CourtOwnerResponse(
    Guid Id,
    string Name,
    string Email,
    string Phone,
    string Document,
    DateTimeOffset CreatedAt);
