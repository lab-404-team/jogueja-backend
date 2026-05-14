using Core.Domain.Primitives;
using CourtOwners.Domain.CourtOwners.Events;

namespace CourtOwners.Domain.CourtOwners;

public sealed class CourtOwner : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string Document { get; private set; } = string.Empty;

    private CourtOwner() { }

    public static CourtOwner Create(string name, string email, string passwordHash, string phone, string document)
    {
        var courtOwner = new CourtOwner();
        var courtOwnerId = Guid.NewGuid();
        courtOwner.RaiseEvent(version => new CourtOwnerCreated(courtOwnerId, name, email, passwordHash, phone, document, version));
        return courtOwner;
    }

    protected override void ApplyEvent(IDomainEvent @event)
    {
        switch (@event)
        {
            case CourtOwnerCreated e:
                Id = e.CourtOwnerId;
                Name = e.Name;
                Email = e.Email;
                PasswordHash = e.PasswordHash;
                Phone = e.Phone;
                Document = e.Document;
                break;
        }
    }
}
