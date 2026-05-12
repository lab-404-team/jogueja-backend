using Core.Shared.Errors;
using FluentValidation;

namespace Core.Application.Extensions
{
    public static class RuleBuilderOptionsExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Error error) =>
            rule.WithErrorCode(error.Code).WithMessage(error.Message);
    }
}
