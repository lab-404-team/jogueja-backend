using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using CourtOwners.Application.CourtOwners.Queries.GetById;
using Jogueja.Api.Endpoints.Routes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Jogueja.Api.Endpoints.CourtOwners;

[ApiVersion("1")]
public sealed class GetCourtOwnerByIdEndpoint(ISender sender) : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<CourtOwnerResponse>
{
    [Authorize]
    [HttpGet(CourtOwnersRoutes.GetById)]
    [SwaggerOperation(Summary = "Get court owner by ID", Tags = [Tags.CourtOwners])]
    public override async Task<ActionResult<CourtOwnerResponse>> HandleAsync(
        [FromRoute] Guid courtOwnerId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCourtOwnerByIdQuery(courtOwnerId);
        var result = await sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : this.HandleFailure(result);
    }
}
