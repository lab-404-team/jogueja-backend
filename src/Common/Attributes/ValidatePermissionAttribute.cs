using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Common.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
public class ValidatePermissionAttribute : AuthorizeAttribute
{
    public ValidatePermissionAttribute(params string[] permissions)
    {
        Policy = $"ValidateAnyPermission_{string.Join("|", permissions)}";
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
    }
}
