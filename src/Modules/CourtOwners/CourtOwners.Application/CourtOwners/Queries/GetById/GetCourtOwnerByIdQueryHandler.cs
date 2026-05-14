using Core.Application.Messaging;
using Core.Domain.Projection;
using Core.Shared.Errors;
using Core.Shared.Results;

namespace CourtOwners.Application.CourtOwners.Queries.GetById;

internal sealed class GetCourtOwnerByIdQueryHandler(IProjection<CourtOwnerReadModel> projection)
    : IQueryHandler<GetCourtOwnerByIdQuery, CourtOwnerResponse>
{
    public async Task<Result<CourtOwnerResponse>> Handle(GetCourtOwnerByIdQuery request, CancellationToken cancellationToken)
    {
        var courtOwner = await projection.GetAsync(request.CourtOwnerId, cancellationToken);

        if (courtOwner is null || courtOwner.IsDeleted)
            return Result.Failure<CourtOwnerResponse>(new NotFoundError(new Error("CourtOwner.NotFound", "Court owner not found.")));

        return new CourtOwnerResponse(
            courtOwner.Id,
            courtOwner.Name,
            courtOwner.Email,
            courtOwner.Phone,
            courtOwner.Document,
            courtOwner.CreatedAt);
    }
}
