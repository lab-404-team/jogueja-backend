using Core.Application.Messaging;
using Core.Application.Services;
using Core.Shared.Errors;
using Core.Shared.Results;
using CourtOwners.Domain.CourtOwners;

namespace CourtOwners.Application.CourtOwners.Commands.Register;

internal sealed class RegisterCourtOwnerCommandHandler(
    ICourtOwnerRepository repository,
    IPasswordService passwordService) : ICommandHandler<RegisterCourtOwnerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterCourtOwnerCommand request, CancellationToken cancellationToken)
    {
        var emailTaken = await repository.EmailExistsAsync(request.Email, cancellationToken);
        if (emailTaken)
            return Result.Failure<Guid>(new ConflictError(new Error("CourtOwner.EmailTaken", "Email already registered.")));

        var passwordHash = passwordService.Hash(request.Password);
        var courtOwner = CourtOwner.Create(request.Name, request.Email, passwordHash, request.Phone, request.Document);

        await repository.SaveAsync(courtOwner, cancellationToken);

        return courtOwner.Id;
    }
}
