using Core.Application.Infrastructure;
using Core.Shared.Errors;
using Core.Shared.Results;
using FluentValidation;
using MediatR;

namespace Core.Application.Behaviors
{
    public sealed class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!validators.Any()) return await next();

            Error[] errors = Validate(new ValidationContext<TRequest>(request));

            if (errors.Length > 0) return ValidationResultFactory.Create<TResponse>(errors);

            return await next();
        }

        private Error[] Validate(IValidationContext validationContext) =>
            validators.Select(validator => validator.Validate(validationContext))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure is not null)
                .Select(validationFailure => new Error(validationFailure.ErrorCode, validationFailure.ErrorMessage))
                .Distinct()
                .ToArray();
    }
}
