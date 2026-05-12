using Common.Extensions;

namespace Common.Helpers;

public static class TokenHelper
{
    public static string ExtractSubjectFromToken(string token)
        => GetContentFromToken(token, "sub");

    public static string ExtractRoleFromToken(string token)
        => GetContentFromToken(token, "roles");

    public static string ExtractClientIdFromToken(string token)
        => GetContentFromToken(token, "client_id");

    public static string GetContentFromToken(string token, string claim)
        => RemoveBearerFromToken(token).GetClaim(claim);

    private static string RemoveBearerFromToken(string token)
        => token.Replace("Bearer ", "");
}
