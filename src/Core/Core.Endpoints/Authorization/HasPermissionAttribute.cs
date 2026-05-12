using Microsoft.AspNetCore.Authorization;

namespace Core.Endpoints.Authorization
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
    {
        public string Permission { get; } = permission;
    }
}
