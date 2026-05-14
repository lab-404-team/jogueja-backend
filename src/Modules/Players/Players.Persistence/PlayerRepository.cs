using Core.Application;
using Core.Application.EventStore;
using Core.Application.ServiceLifetimes;
using Core.Domain.EventStore;
using Core.Domain.Projection;
using Players.Application.Players;
using Players.Application.Players.Queries.GetById;
using Players.Domain.Players;

namespace Players.Persistence;

internal sealed class PlayerRepository(
    IEventStore<PlayerDbContext> eventStore,
    IUnitOfWork<PlayerDbContext> unitOfWork,
    IProjection<PlayerReadModel> projection) : IPlayerRepository, IScoped
{
    public async Task SaveAsync(Player player, CancellationToken cancellationToken)
    {
        await unitOfWork.ExecuteAsync(async ct =>
        {
            while (player.TryDequeueEvent(out var @event))
                await eventStore.AppendAsync(StoreEvent<Player>.Create(player, @event), ct);
        }, cancellationToken);

        var readModel = new PlayerReadModel
        {
            Id = player.Id,
            Name = player.Name,
            Email = player.Email,
            PasswordHash = player.PasswordHash,
            Phone = player.Phone,
            CreatedAt = player.CreatedAt,
            IsDeleted = false
        };

        await projection.ReplaceInsertAsync(readModel, cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        var existing = await projection.FindAsync(p => p.Email == email, cancellationToken);
        return existing is not null;
    }
}
