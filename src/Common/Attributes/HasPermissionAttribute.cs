using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Common.Attributes;

public record PermissionItem(string Id, string Role);

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class HasPermissionAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _claimNames;
    private readonly string[] _allowedRoles;

    public HasPermissionAttribute(string[] claimNames, string[] allowedRoles)
    {
        _claimNames = claimNames;
        _allowedRoles = allowedRoles;
    }

    public HasPermissionAttribute(params string[] claimNamesAndRoles)
    {
        var splitIndex = claimNamesAndRoles.TakeWhile(s => s.Contains('/')).Count();
        _claimNames = [.. claimNamesAndRoles.Take(splitIndex)];
        _allowedRoles = [.. claimNamesAndRoles.Skip(splitIndex)];
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user is null)
        {
            context.Result = new ForbidResult();
            return;
        }

        if (!context.RouteData.Values.TryGetValue("id", out var idObj) || idObj is null)
        {
            context.Result = new ForbidResult();
            return;
        }

        if (!Guid.TryParse(idObj.ToString(), out Guid resourceId))
        {
            context.Result = new ForbidResult();
            return;
        }

        foreach (var claimName in _claimNames)
        {
            var claim = user.Claims.FirstOrDefault(c => c.Type == claimName)?.Value;

            if (string.IsNullOrWhiteSpace(claim))
                continue;

            List<PermissionItem>? permissions;

            try
            {
                permissions = JsonConvert.DeserializeObject<List<PermissionItem>>(claim);
            }
            catch
            {
                continue;
            }

            if (permissions!.Any(p =>
                Guid.TryParse(p.Id, out var permissionId) &&
                permissionId == resourceId &&
                _allowedRoles.Contains(p.Role, StringComparer.OrdinalIgnoreCase)))
            {
                return;
            }
        }

        context.Result = new ForbidResult();
    }
}
