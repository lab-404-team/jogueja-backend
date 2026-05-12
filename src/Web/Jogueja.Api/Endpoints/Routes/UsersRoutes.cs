namespace Jogueja.Api.Endpoints.Routes
{
    internal class UsersRoutes
    {
        internal const string BaseUri = "v{version:apiVersion}/Users";

        internal const string UserId = "UserId";

        internal const string CreateUser = $"{BaseUri}/create-user";
        internal const string ResetToken = $"{BaseUri}/{{{UserId}:guid}}/reset-token";
        internal const string DeleteUser = $"{BaseUri}/{{{UserId}:guid}}";
        internal const string UpdateProfile = $"{BaseUri}/profile";
        internal const string UpsertAddress = $"{BaseUri}/address";

        internal const string ChangeRole = $"{BaseUri}/{{{UserId}:guid}}/role";

        internal const string GetUserById = $"{BaseUri}/{{{UserId}:guid}}";
        internal const string GetUserByDocument = $"{BaseUri}/document";
        internal const string ListUser = $"{BaseUri}/users";
        internal const string ListUserByName = $"{BaseUri}/name/users";
        internal const string PagedUser = $"{BaseUri}/paged-users";
    }
}
