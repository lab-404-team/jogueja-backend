using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using Jogueja.Api.Endpoints.Routes;
using Jogueja.Api.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Players.Application.Players.Commands.Login;
using Swashbuckle.AspNetCore.Annotations;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;

namespace Jogueja.Api.Endpoints.Players;

public sealed class LoginPlayerEndpoint(ISender sender, ITokenService tokenService) : EndpointBaseAsync
    .WithRequest<LoginPlayerRequest>
    .WithActionResult<LoginPlayerResponse>
{
    [ApiVersion("1")]
    [HttpPost(PlayersRoutes.Login)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Authenticate a player",
        Description = "Authenticate a player based on the provided credentials.",
        Tags = [Tags.Players])]
    public override async Task<ActionResult<LoginPlayerResponse>> HandleAsync(
        [FromBody] LoginPlayerRequest request,
        CancellationToken cancellationToken = default)
        => await Result.Create(request)
            .Map(req => new LoginPlayerCommand(req.Email, req.Password))
            .Bind(cmd => sender.Send(cmd, cancellationToken))
            .Map(auth => new LoginPlayerResponse(
                auth.PlayerId, auth.Name,
                tokenService.GenerateToken(auth.PlayerId.ToString(), auth.Role)))
            .Match(result => Ok(result), this.HandleFailure);
}

public sealed record LoginPlayerRequest(string Email, string Password);

public sealed record LoginPlayerResponse(Guid PlayerId, string Name, string Token);
