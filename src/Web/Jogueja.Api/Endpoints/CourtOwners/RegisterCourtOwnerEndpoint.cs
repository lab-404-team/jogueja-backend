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

public sealed class RegisterCourtOwnerEndpoint(ISender sender) : EndpointBaseAsync
    .WithRequest<RegisterCourtOwnerRequest>
    .WithActionResult<Guid>
{
    [ApiVersion("1")]
    [HttpPost(CourtOwnersRoutes.Register)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Register a new court owner",
        Description = "Register a new court owner based on the provided request data.",
        Tags = [Tags.CourtOwners])]
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
