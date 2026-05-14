namespace Common.Authorization
{
    public static class RolePermissions
    {
        public static readonly IReadOnlyDictionary<string, string[]> Map =
            new Dictionary<string, string[]>
            {
                ["Player"] = [Permissions.Players.Read],
                ["CourtOwner"] = [Permissions.CourtOwners.Read],
            };

        public static string[] GetPermissions(string role)
            => Map.TryGetValue(role, out var permissions) ? permissions : [];
    }
}
