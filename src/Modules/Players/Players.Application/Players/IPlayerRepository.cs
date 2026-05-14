using Players.Domain.Players;

namespace Players.Application.Players;

public interface IPlayerRepository
{
    Task SaveAsync(Player player, CancellationToken cancellationToken);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
}
