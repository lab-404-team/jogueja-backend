using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using CourtOwners.Application.CourtOwners.Queries.GetById;
using Jogueja.Api.Endpoints.Routes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Jogueja.Api.Endpoints.CourtOwners;

public sealed class GetCourtOwnerByIdEndpoint(ISender sender) : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<CourtOwnerResponse>
{
    [ApiVersion("1")]
    [HttpGet(CourtOwnersRoutes.GetById)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Get court owner by ID",
        Description = "Get court owner by ID based on the provided request data.",
        Tags = [Tags.CourtOwners])]
    [Authorize]
    public override async Task<ActionResult<CourtOwnerResponse>> HandleAsync(
        [FromRoute] Guid courtOwnerId,
        CancellationToken cancellationToken = default)
        => await Result.Create(new GetCourtOwnerByIdQuery(courtOwnerId))
            .Bind(query => sender.Send(query, cancellationToken))
            .Match(result => Ok(result), this.HandleFailure);
}
