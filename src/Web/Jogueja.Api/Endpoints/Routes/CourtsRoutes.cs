namespace Jogueja.Api.Endpoints.Routes
{
    internal class CourtsRoutes
    {
        internal const string BaseUri = "v{version:apiVersion}/Courts";
        internal const string CourtId = "CourtId";

        internal const string CreateCourt = $"{BaseUri}/create-court";
        internal const string UpdateCourt = $"{BaseUri}/{{{CourtId}:guid}}";
        internal const string DeactivateCourt = $"{BaseUri}/{{{CourtId}:guid}}/deactivate";
        internal const string GetCourtById = $"{BaseUri}/{{{CourtId}:guid}}";
        internal const string ListCourts = $"{BaseUri}/courts";
        internal const string PagedCourts = $"{BaseUri}/paged-courts";
    }
}
