using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using CourtOwners.Application.CourtOwners.Commands.Register;
using Jogueja.Api.Endpoints.Routes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Jogueja.Api.Endpoints.CourtOwners;

[ApiVersion("1")]
public sealed class RegisterCourtOwnerEndpoint(ISender sender) : EndpointBaseAsync
    .WithRequest<RegisterCourtOwnerRequest>
    .WithActionResult<Guid>
{
    [HttpPost(CourtOwnersRoutes.Register)]
    [SwaggerOperation(Summary = "Register a new court owner", Tags = [Tags.CourtOwners])]
    public override async Task<ActionResult<Guid>> HandleAsync(
        [FromBody] RegisterCourtOwnerRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new RegisterCourtOwnerCommand(
            request.Name, request.Email, request.Password, request.Phone, request.Document);
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(HandleAsync), new { courtOwnerId = result.Value }, result.Value)
            : this.HandleFailure(result);
    }
}

public sealed record RegisterCourtOwnerRequest(
    string Name, string Email, string Password, string Phone, string Document);
