using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
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
        => await Result.Create(request)
            .Map(req => new RegisterCourtOwnerCommand(req.Name, req.Email, req.Password, req.Phone, req.Document))
            .Bind(cmd => sender.Send(cmd, cancellationToken))
            .Match(id => CreatedAtAction(nameof(HandleAsync), new { courtOwnerId = id }, id), this.HandleFailure);
}

public sealed record RegisterCourtOwnerRequest(
    string Name, string Email, string Password, string Phone, string Document);
