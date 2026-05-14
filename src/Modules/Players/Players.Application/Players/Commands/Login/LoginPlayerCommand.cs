using Core.Application.Messaging;

namespace Players.Application.Players.Commands.Login;

public sealed record LoginPlayerCommand(string Email, string Password) : ICommand<PlayerAuthResult>;

public sealed record PlayerAuthResult(Guid PlayerId, string Name, string Role);
