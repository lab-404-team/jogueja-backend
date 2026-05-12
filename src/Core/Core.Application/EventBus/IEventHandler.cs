using Core.Domain.Primitives;

namespace Core.Application.EventBus
{
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        Task Handle(TEvent @event, CancellationToken cancellationToken = default);
    }

    public interface IEventHandler
    {
        Task Handle(IEvent @event, CancellationToken cancellationToken = default);
    }
}
