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
        => await Result.Create(request)
            .Map(req => new LoginCourtOwnerCommand(req.Email, req.Password))
            .Bind(cmd => sender.Send(cmd, cancellationToken))
            .Map(auth => new LoginCourtOwnerResponse(
                auth.CourtOwnerId, auth.Name,
                tokenService.GenerateToken(auth.CourtOwnerId.ToString(), auth.Role)))
            .Match(ControllerBase.Ok, this.HandleFailure);
}

public sealed record LoginCourtOwnerRequest(string Email, string Password);

public sealed record LoginCourtOwnerResponse(Guid CourtOwnerId, string Name, string Token);
