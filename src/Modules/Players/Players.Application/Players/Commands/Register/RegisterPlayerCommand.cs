using Core.Application.Messaging;

namespace Players.Application.Players.Commands.Register;

public sealed record RegisterPlayerCommand(
    string Name,
    string Email,
    string Password,
    string Phone) : ICommand<Guid>;
