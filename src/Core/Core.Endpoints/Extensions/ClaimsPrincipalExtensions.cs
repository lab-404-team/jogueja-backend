using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Endpoints.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid ExtractAuth0IdFromToken(string token)
            => Guid.Parse(token.GetIdentityProviderId("sub").Split('|')[1]);

        internal static string GetIdentityProviderId(this string jwtToken, string claimName)
            => new JwtSecurityTokenHandler().ReadJwtToken(jwtToken).Claims.SingleOrDefault(claim => claim.Type == claimName)?.Value ?? string.Empty;
    }
}
