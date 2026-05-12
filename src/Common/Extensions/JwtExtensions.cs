using System.IdentityModel.Tokens.Jwt;

namespace Common.Extensions;

public static class JwtExtensions
{
    public static string GetClaim(this string jwtToken, string claimName)
        => new JwtSecurityTokenHandler().ReadJwtToken(jwtToken).Claims.FirstOrDefault(c => c.Type == claimName)?.Value;
}
