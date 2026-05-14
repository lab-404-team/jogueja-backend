using Core.Application.Messaging;
using Core.Domain.Projection;
using Core.Shared.Errors;
using Core.Shared.Results;

namespace Players.Application.Players.Queries.GetById;

internal sealed class GetPlayerByIdQueryHandler(IProjection<PlayerReadModel> projection)
    : IQueryHandler<GetPlayerByIdQuery, PlayerResponse>
{
    public async Task<Result<PlayerResponse>> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        var player = await projection.GetAsync(request.PlayerId, cancellationToken);

        if (player is null || player.IsDeleted)
            return Result.Failure<PlayerResponse>(new NotFoundError(new Error("Player.NotFound", "Player not found.")));

        return new PlayerResponse(player.Id, player.Name, player.Email, player.Phone, player.CreatedAt);
    }
}
