using Core.Application.Messaging;

namespace CourtOwners.Application.CourtOwners.Commands.Register;

public sealed record RegisterCourtOwnerCommand(
    string Name,
    string Email,
    string Password,
    string Phone,
    string Document) : ICommand<Guid>;
