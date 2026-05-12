using Microsoft.AspNetCore.Authorization;

namespace Common.Authorization;

public class PermissionRequirement(string[] permissions) : IAuthorizationRequirement
{
    public string[] Permissions { get; } = permissions;
}
