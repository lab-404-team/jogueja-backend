using Core.Application.Messaging;
using Core.Application.Services;
using Core.Domain.Projection;
using Core.Shared.Errors;
using Core.Shared.Results;
using CourtOwners.Application.CourtOwners;

namespace CourtOwners.Application.CourtOwners.Commands.Login;

internal sealed class LoginCourtOwnerCommandHandler(
    IProjection<CourtOwnerReadModel> projection,
    IPasswordService passwordService) : ICommandHandler<LoginCourtOwnerCommand, CourtOwnerAuthResult>
{
    private const string Role = "CourtOwner";

    public async Task<Result<CourtOwnerAuthResult>> Handle(LoginCourtOwnerCommand request, CancellationToken cancellationToken)
    {
        var courtOwner = await projection.FindAsync(c => c.Email == request.Email, cancellationToken);

        if (courtOwner is null || courtOwner.IsDeleted)
            return Result.Failure<CourtOwnerAuthResult>(new NotFoundError(new Error("CourtOwner.NotFound", "Invalid credentials.")));

        if (!passwordService.Verify(courtOwner.PasswordHash, request.Password))
            return Result.Failure<CourtOwnerAuthResult>(new Error("CourtOwner.InvalidPassword", "Invalid credentials."));

        return new CourtOwnerAuthResult(courtOwner.Id, courtOwner.Name, Role);
    }
}
