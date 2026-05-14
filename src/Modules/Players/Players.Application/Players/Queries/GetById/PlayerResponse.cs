namespace Players.Application.Players.Queries.GetById;

public sealed record PlayerResponse(
    Guid Id,
    string Name,
    string Email,
    string Phone,
    DateTimeOffset CreatedAt);
