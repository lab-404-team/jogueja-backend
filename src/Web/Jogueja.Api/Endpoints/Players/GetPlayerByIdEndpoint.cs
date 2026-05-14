using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using Jogueja.Api.Endpoints.Routes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Players.Application.Players.Queries.GetById;
using Swashbuckle.AspNetCore.Annotations;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;

namespace Jogueja.Api.Endpoints.Players;

[ApiVersion("1")]
public sealed class GetPlayerByIdEndpoint(ISender sender) : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<PlayerResponse>
{
    [ApiVersion("1.0")]
    [HttpGet(PlayersRoutes.GetPlayerById)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
    Summary = "Get player by ID",
    Description = "Get player by ID based on the provided request data.",
    Tags = [Tags.Players])]
    [Authorize]
    public override async Task<ActionResult<PlayerResponse>> HandleAsync(
        [FromRoute] Guid playerId,
        CancellationToken cancellationToken = default)
        => await Result.Create(new GetPlayerByIdQuery(playerId))
        .Bind(query => sender.Send(query, cancellationToken))
        .Match(result => Ok(result), this.HandleFailure);
}
