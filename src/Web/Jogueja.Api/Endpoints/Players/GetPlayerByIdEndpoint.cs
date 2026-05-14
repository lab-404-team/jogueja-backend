using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using Jogueja.Api.Endpoints.Routes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Players.Application.Players.Queries.GetById;
using Swashbuckle.AspNetCore.Annotations;

namespace Jogueja.Api.Endpoints.Players;

[ApiVersion("1")]
public sealed class GetPlayerByIdEndpoint(ISender sender) : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<PlayerResponse>
{
    [Authorize]
    [HttpGet(PlayersRoutes.GetById)]
    [SwaggerOperation(Summary = "Get player by ID", Tags = [Tags.Players])]
    public override async Task<ActionResult<PlayerResponse>> HandleAsync(
        [FromRoute] Guid playerId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPlayerByIdQuery(playerId);
        var result = await sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : this.HandleFailure(result);
    }
}
