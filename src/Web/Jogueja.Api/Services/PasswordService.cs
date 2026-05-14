using Core.Application.Services;
using Microsoft.AspNetCore.Identity;

namespace Jogueja.Api.Services;

internal sealed class PasswordService : IPasswordService
{
    private readonly PasswordHasher<string> _hasher = new();

    public string Hash(string password) => _hasher.HashPassword(string.Empty, password);

    public bool Verify(string hash, string password)
        => _hasher.VerifyHashedPassword(string.Empty, hash, password) != PasswordVerificationResult.Failed;
}
