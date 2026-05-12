using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Common.Authorization;

public class PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
    : DefaultAuthorizationPolicyProvider(options)
{
    private const string AnyPermissionPrefix = "ValidateAnyPermission_";

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!policyName.StartsWith(AnyPermissionPrefix))
            return await base.GetPolicyAsync(policyName);

        var permissions = policyName[AnyPermissionPrefix.Length..].Split('|', StringSplitOptions.RemoveEmptyEntries);

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(permissions))
            .RequireAuthenticatedUser()
            .Build();
    }
}
