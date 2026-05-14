using Core.Application.Messaging;

namespace Players.Application.Players.Queries.GetById;

public sealed record GetPlayerByIdQuery(Guid PlayerId) : IQuery<PlayerResponse>;
