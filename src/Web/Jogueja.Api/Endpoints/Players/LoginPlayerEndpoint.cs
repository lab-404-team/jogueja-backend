using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using Jogueja.Api.Endpoints.Routes;
using Jogueja.Api.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Players.Application.Players.Commands.Login;
using Swashbuckle.AspNetCore.Annotations;

namespace Jogueja.Api.Endpoints.Players;

[ApiVersion("1")]
public sealed class LoginPlayerEndpoint(ISender sender, ITokenService tokenService) : EndpointBaseAsync
    .WithRequest<LoginPlayerRequest>
    .WithActionResult<LoginPlayerResponse>
{
    [HttpPost(PlayersRoutes.Login)]
    [SwaggerOperation(Summary = "Authenticate a player", Tags = [Tags.Players])]
    public override async Task<ActionResult<LoginPlayerResponse>> HandleAsync(
        [FromBody] LoginPlayerRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new LoginPlayerCommand(request.Email, request.Password);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return this.HandleFailure(result);

        var token = tokenService.GenerateToken(result.Value.PlayerId.ToString(), result.Value.Role);
        return Ok(new LoginPlayerResponse(result.Value.PlayerId, result.Value.Name, token));
    }
}

public sealed record LoginPlayerRequest(string Email, string Password);

public sealed record LoginPlayerResponse(Guid PlayerId, string Name, string Token);
