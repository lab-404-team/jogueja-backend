using Core.Application.Messaging;

namespace CourtOwners.Application.CourtOwners.Queries.GetById;

public sealed record GetCourtOwnerByIdQuery(Guid CourtOwnerId) : IQuery<CourtOwnerResponse>;
