using CourtOwners.Domain.CourtOwners;

namespace CourtOwners.Application.CourtOwners;

public interface ICourtOwnerRepository
{
    Task SaveAsync(CourtOwner courtOwner, CancellationToken cancellationToken);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
}
