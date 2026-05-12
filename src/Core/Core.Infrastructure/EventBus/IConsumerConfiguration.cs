using MassTransit;

namespace Core.Infrastructure.EventBus
{
    public interface IConsumerConfiguration
    {
        void AddConsumers(IRegistrationConfigurator registrationConfigurator);
    }
}
