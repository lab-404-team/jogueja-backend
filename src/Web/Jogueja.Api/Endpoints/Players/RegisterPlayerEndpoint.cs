using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using Jogueja.Api.Endpoints.Routes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Players.Application.Players.Commands.Register;
using Swashbuckle.AspNetCore.Annotations;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;

namespace Jogueja.Api.Endpoints.Players;

public sealed class RegisterPlayerEndpoint(ISender sender) : EndpointBaseAsync
    .WithRequest<RegisterPlayerRequest>
    .WithActionResult<Guid>
{
    [ApiVersion("1")]
    [HttpPost(PlayersRoutes.Register)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Register a new player",
        Description = "Register a new player based on the provided request data.",
        Tags = [Tags.Players])]
    public override async Task<ActionResult<Guid>> HandleAsync(
        [FromBody] RegisterPlayerRequest request,
        CancellationToken cancellationToken = default)
        => await Result.Create(request)
            .Map(req => new RegisterPlayerCommand(req.Name, req.Email, req.Password, req.Phone))
            .Bind(cmd => sender.Send(cmd, cancellationToken))
            .Match(id => CreatedAtAction(nameof(HandleAsync), new { playerId = id }, id), this.HandleFailure);
}

public sealed record RegisterPlayerRequest(string Name, string Email, string Password, string Phone);
