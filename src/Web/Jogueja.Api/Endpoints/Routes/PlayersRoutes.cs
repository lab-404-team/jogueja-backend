namespace Jogueja.Api.Endpoints.Routes;

internal static class PlayersRoutes
{
    internal const string BaseUri = "v{version:apiVersion}/players";

    internal const string Register = $"{BaseUri}/register";
    internal const string Login = $"{BaseUri}/login";
    internal const string GetById = $"{BaseUri}/{{playerId:guid}}";
}
