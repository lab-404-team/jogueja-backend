namespace Common.Authorization
{
    public static class RolePermissions
    {
        public static readonly IReadOnlyDictionary<string, string[]> Map =
            new Dictionary<string, string[]>();

        public static string[] GetPermissions(string role)
            => Map.TryGetValue(role, out var permissions) ? permissions : [];
    }
}
