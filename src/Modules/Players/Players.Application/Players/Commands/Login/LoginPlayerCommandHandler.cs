using Core.Application.Messaging;
using Core.Application.Services;
using Core.Domain.Projection;
using Core.Shared.Errors;
using Core.Shared.Results;
using Players.Application.Players.Queries.GetById;

namespace Players.Application.Players.Commands.Login;

internal sealed class LoginPlayerCommandHandler(
    IProjection<PlayerReadModel> projection,
    IPasswordService passwordService) : ICommandHandler<LoginPlayerCommand, PlayerAuthResult>
{
    private const string Role = "Player";

    public async Task<Result<PlayerAuthResult>> Handle(LoginPlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await projection.FindAsync(p => p.Email == request.Email, cancellationToken);

        if (player is null || player.IsDeleted)
            return Result.Failure<PlayerAuthResult>(new NotFoundError(new Error("Player.NotFound", "Invalid credentials.")));

        if (!passwordService.Verify(player.PasswordHash, request.Password))
            return Result.Failure<PlayerAuthResult>(new Error("Player.InvalidPassword", "Invalid credentials."));

        return new PlayerAuthResult(player.Id, player.Name, Role);
    }
}
