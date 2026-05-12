using Core.Domain.Projection;
using DocumentValidator;
using FluentValidation;
using MediatR;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Common.Validators;

public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
{
    private const int TimeoutRegexInMilliseconds = 250;
    private const int MAX_FILE_SIZE_MB = 5;
    private const int MAX_FILE_SIZE_BYTES = MAX_FILE_SIZE_MB * 1024 * 1024;

    protected static bool BeAValidCnpj(string? cnpj)
        => string.IsNullOrWhiteSpace(cnpj) || CnpjValidation.Validate(cnpj);

    protected static bool BeAValidCpf(string cpf)
        => CpfValidation.Validate(cpf);

    protected static bool BeAValidAgencyNumber(string agencyNumber)
        => Regex.IsMatch(agencyNumber ?? string.Empty, @"^\d{1,4}$");

    protected static bool BeAValidPhone(string phone)
    {
        try
        {
            string pattern = @"^\+\d{2} \(\d{2}\) \d{5}-\d{4}$";
            var match = Regex.Match(phone, pattern, RegexOptions.None, TimeSpan.FromMilliseconds(TimeoutRegexInMilliseconds));
            return match.Success;
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    protected async Task<bool> NotExistsAsync<TProjection>(
        IProjection<TProjection> projection,
        Expression<Func<TProjection, bool>> predicate,
        CancellationToken cancellationToken) where TProjection : Core.Domain.Primitives.IProjection
    {
        var result = await projection.FindAsync(predicate, cancellationToken);
        return result is null;
    }

    protected async Task<bool> NotExistsAsync<TResponse>(
       ISender sender,
       IRequest<TResponse> query,
       CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);

        if (result is null)
            return true;

        var isFailureProperty = typeof(TResponse).GetProperty("IsFailure");
        return isFailureProperty?.GetValue(result) as bool? ?? false;
    }

    protected static bool BeAValidEmail(string email)
    {
        try
        {
            string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            var match = Regex.Match(email, pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(TimeoutRegexInMilliseconds));
            return match.Success;
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    protected static bool BeAtLeast18YearsOld(DateTimeOffset dateOfBirth)
    {
        var today = DateTimeOffset.UtcNow.Date;
        var birthDate = dateOfBirth.Date;
        var age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age)) age--;
        return age >= 18;
    }

    protected static bool BeAtMost75YearsOld(DateTimeOffset dateOfBirth)
    {
        var today = DateTimeOffset.UtcNow.Date;
        var birthDate = dateOfBirth.Date;
        var age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age)) age--;
        return age <= 75;
    }

    protected static bool BeAValidCep(string? cep)
    {
        if (string.IsNullOrWhiteSpace(cep))
            return false;

        var digits = Regex.Replace(cep.Trim(), @"\D", "");
        return digits.Length == 8;
    }

    public static List<string> GetAvailableUfs() =>
    [
        "SP", "AC", "AL", "AM", "AP", "BA", "DF", "ES",
        "GO", "MA", "MG", "MS", "MT", "PB", "PE", "PI",
        "PR", "RJ", "RN", "RO", "RR", "RS", "SC", "SE",
        "TO", "PA", "CE"
    ];

    protected static List<string> GetAllowedFileExtensions() =>
    [
        ".png", ".jpeg", ".jpg", ".pdf"
    ];

    protected bool BeValidFileSize(byte[] file)
    {
        try
        {
            using var memoryStream = new MemoryStream(file);
            return memoryStream.Length <= MAX_FILE_SIZE_BYTES;
        }
        catch
        {
            return false;
        }
    }

    protected bool BeValidFileExtension(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        return GetAllowedFileExtensions().Contains(extension.ToLower());
    }
}
