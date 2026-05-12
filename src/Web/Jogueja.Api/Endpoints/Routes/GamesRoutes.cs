namespace Jogueja.Api.Endpoints.Routes
{
    internal class GamesRoutes
    {
        internal const string BaseUri = "v{version:apiVersion}/Games";
        internal const string GameId = "GameId";
        internal const string OrganizerId = "OrganizerId";

        internal const string CreateGame = $"{BaseUri}/create-game";
        internal const string CancelGame = $"{BaseUri}/{{{GameId}:guid}}/cancel";
        internal const string FinishGame = $"{BaseUri}/{{{GameId}:guid}}/finish";
        internal const string SetGamePrice = $"{BaseUri}/{{{GameId}:guid}}/price";
        internal const string InvitePlayer = $"{BaseUri}/{{{GameId}:guid}}/players/invite";
        internal const string RequestJoin = $"{BaseUri}/{{{GameId}:guid}}/players/request";
        internal const string AcceptInvite = $"{BaseUri}/{{{GameId}:guid}}/players/accept";
        internal const string DeclineInvite = $"{BaseUri}/{{{GameId}:guid}}/players/decline";
        internal const string GetGameById = $"{BaseUri}/{{{GameId}:guid}}";
        internal const string ListGames = $"{BaseUri}/games";
        internal const string ListOwnGames = $"{BaseUri}/own-games";
        internal const string PagedGames = $"{BaseUri}/paged-games";
    }
}
