using Core.Application.Messaging;
using Core.Application.Services;
using Core.Shared.Errors;
using Core.Shared.Results;
using Players.Domain.Players;

namespace Players.Application.Players.Commands.Register;

internal sealed class RegisterPlayerCommandHandler(
    IPlayerRepository repository,
    IPasswordService passwordService) : ICommandHandler<RegisterPlayerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterPlayerCommand request, CancellationToken cancellationToken)
    {
        var emailTaken = await repository.EmailExistsAsync(request.Email, cancellationToken);
        if (emailTaken)
            return Result.Failure<Guid>(new ConflictError(new Error("Player.EmailTaken", "Email already registered.")));

        var passwordHash = passwordService.Hash(request.Password);
        var player = Player.Create(request.Name, request.Email, passwordHash, request.Phone);

        await repository.SaveAsync(player, cancellationToken);

        return player.Id;
    }
}
