namespace Jogueja.Api.Endpoints.Routes;

internal static class CourtOwnersRoutes
{
    internal const string BaseUri = "v{version:apiVersion}/court-owners";

    internal const string Register = $"{BaseUri}/register";
    internal const string Login = $"{BaseUri}/login";
    internal const string GetById = $"{BaseUri}/{{courtOwnerId:guid}}";
}
