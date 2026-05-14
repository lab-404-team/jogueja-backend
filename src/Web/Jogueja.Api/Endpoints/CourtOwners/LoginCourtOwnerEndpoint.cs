using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using CourtOwners.Application.CourtOwners.Commands.Login;
using Jogueja.Api.Endpoints.Routes;
using Jogueja.Api.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;

namespace Jogueja.Api.Endpoints.CourtOwners;

public sealed class LoginCourtOwnerEndpoint(ISender sender, ITokenService tokenService) : EndpointBaseAsync
    .WithRequest<LoginCourtOwnerRequest>
    .WithActionResult<LoginCourtOwnerResponse>
{
    [ApiVersion("1")]
    [HttpPost(CourtOwnersRoutes.Login)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Authenticate a court owner",
        Description = "Authenticate a court owner based on the provided credentials.",
        Tags = [Tags.CourtOwners])]
    public override async Task<ActionResult<LoginCourtOwnerResponse>> HandleAsync(
        [FromBody] LoginCourtOwnerRequest request,
        CancellationToken cancellationToken = default)
        => await Result.Create(request)
            .Map(req => new LoginCourtOwnerCommand(req.Email, req.Password))
            .Bind(cmd => sender.Send(cmd, cancellationToken))
            .Map(auth => new LoginCourtOwnerResponse(
                auth.CourtOwnerId, auth.Name,
                tokenService.GenerateToken(auth.CourtOwnerId.ToString(), auth.Role)))
            .Match(result => Ok(result), this.HandleFailure);
}

public sealed record LoginCourtOwnerRequest(string Email, string Password);

public sealed record LoginCourtOwnerResponse(Guid CourtOwnerId, string Name, string Token);
