using Core.Domain.Primitives;
using Players.Domain.Players.Events;

namespace Players.Domain.Players;

public sealed class Player : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;

    private Player() { }

    public static Player Create(string name, string email, string passwordHash, string phone)
    {
        var player = new Player();
        var playerId = Guid.NewGuid();
        player.RaiseEvent(version => new PlayerCreated(playerId, name, email, passwordHash, phone, version));
        return player;
    }

    protected override void ApplyEvent(IDomainEvent @event)
    {
        switch (@event)
        {
            case PlayerCreated e:
                Id = e.PlayerId;
                Name = e.Name;
                Email = e.Email;
                PasswordHash = e.PasswordHash;
                Phone = e.Phone;
                break;
        }
    }
}
