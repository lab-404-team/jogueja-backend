using MassTransit;

namespace Core.Infrastructure.EventBus
{
    public interface IRequestClientConfiguration
    {
        void AddRequestClients(IRegistrationConfigurator registrationConfigurator);
    }
}
