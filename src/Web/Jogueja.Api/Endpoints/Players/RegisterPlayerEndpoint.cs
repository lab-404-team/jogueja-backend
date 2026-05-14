using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using Jogueja.Api.Endpoints.Routes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Players.Application.Players.Commands.Register;
using Swashbuckle.AspNetCore.Annotations;

namespace Jogueja.Api.Endpoints.Players;

[ApiVersion("1")]
public sealed class RegisterPlayerEndpoint(ISender sender) : EndpointBaseAsync
    .WithRequest<RegisterPlayerRequest>
    .WithActionResult<Guid>
{
    [HttpPost(PlayersRoutes.Register)]
    [SwaggerOperation(Summary = "Register a new player", Tags = [Tags.Players])]
    public override async Task<ActionResult<Guid>> HandleAsync(
        [FromBody] RegisterPlayerRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new RegisterPlayerCommand(request.Name, request.Email, request.Password, request.Phone);
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(HandleAsync), new { playerId = result.Value }, result.Value)
            : this.HandleFailure(result);
    }
}

public sealed record RegisterPlayerRequest(string Name, string Email, string Password, string Phone);
