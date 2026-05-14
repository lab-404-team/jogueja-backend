using Core.Application;
using Core.Application.EventStore;
using Core.Application.ServiceLifetimes;
using Core.Domain.EventStore;
using Core.Domain.Projection;
using CourtOwners.Application.CourtOwners;
using CourtOwners.Domain.CourtOwners;

namespace CourtOwners.Persistence;

internal sealed class CourtOwnerRepository(
    IEventStore<CourtOwnerDbContext> eventStore,
    IUnitOfWork<CourtOwnerDbContext> unitOfWork,
    IProjection<CourtOwnerReadModel> projection) : ICourtOwnerRepository, IScoped
{
    public async Task SaveAsync(CourtOwner courtOwner, CancellationToken cancellationToken)
    {
        await unitOfWork.ExecuteAsync(async ct =>
        {
            while (courtOwner.TryDequeueEvent(out var @event))
                await eventStore.AppendAsync(StoreEvent<CourtOwner>.Create(courtOwner, @event), ct);
        }, cancellationToken);

        var readModel = new CourtOwnerReadModel
        {
            Id = courtOwner.Id,
            Name = courtOwner.Name,
            Email = courtOwner.Email,
            PasswordHash = courtOwner.PasswordHash,
            Phone = courtOwner.Phone,
            Document = courtOwner.Document,
            CreatedAt = courtOwner.CreatedAt,
            IsDeleted = false
        };

        await projection.ReplaceInsertAsync(readModel, cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        var existing = await projection.FindAsync(c => c.Email == email, cancellationToken);
        return existing is not null;
    }
}
