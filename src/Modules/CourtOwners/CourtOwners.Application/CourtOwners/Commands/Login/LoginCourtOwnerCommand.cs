using Core.Application.Messaging;

namespace CourtOwners.Application.CourtOwners.Commands.Login;

public sealed record LoginCourtOwnerCommand(string Email, string Password) : ICommand<CourtOwnerAuthResult>;

public sealed record CourtOwnerAuthResult(Guid CourtOwnerId, string Name, string Role);
