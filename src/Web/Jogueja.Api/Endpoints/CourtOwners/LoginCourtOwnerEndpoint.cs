using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using CourtOwners.Application.CourtOwners.Commands.Login;
using Jogueja.Api.Endpoints.Routes;
using Jogueja.Api.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Jogueja.Api.Endpoints.CourtOwners;

[ApiVersion("1")]
public sealed class LoginCourtOwnerEndpoint(ISender sender, ITokenService tokenService) : EndpointBaseAsync
    .WithRequest<LoginCourtOwnerRequest>
    .WithActionResult<LoginCourtOwnerResponse>
{
    [HttpPost(CourtOwnersRoutes.Login)]
    [SwaggerOperation(Summary = "Authenticate a court owner", Tags = [Tags.CourtOwners])]
    public override async Task<ActionResult<LoginCourtOwnerResponse>> HandleAsync(
        [FromBody] LoginCourtOwnerRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new LoginCourtOwnerCommand(request.Email, request.Password);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return this.HandleFailure(result);

        var token = tokenService.GenerateToken(result.Value.CourtOwnerId.ToString(), result.Value.Role);
        return Ok(new LoginCourtOwnerResponse(result.Value.CourtOwnerId, result.Value.Name, token));
    }
}

public sealed record LoginCourtOwnerRequest(string Email, string Password);

public sealed record LoginCourtOwnerResponse(Guid CourtOwnerId, string Name, string Token);
