using Microsoft.AspNetCore.Authorization;

namespace Common.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var role = context.User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

        if (role is null)
            return Task.CompletedTask;

        var rolePermissions = RolePermissions.GetPermissions(role);

        if (requirement.Permissions.Any(p => rolePermissions.Contains(p)))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
