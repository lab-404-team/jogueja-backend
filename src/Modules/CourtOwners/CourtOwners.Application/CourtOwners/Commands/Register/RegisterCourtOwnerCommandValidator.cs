using FluentValidation;

namespace CourtOwners.Application.CourtOwners.Commands.Register;

internal sealed class RegisterCourtOwnerCommandValidator : AbstractValidator<RegisterCourtOwnerCommand>
{
    public RegisterCourtOwnerCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(100);
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Document).NotEmpty().MaximumLength(20);
    }
}
